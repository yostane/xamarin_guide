# Data persistence in Xamarin using Entity Framework Core

Thanks to .Net Standard 2 and .Net Core, we can take advantage of the astonishing potential of [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/). It is an ORM (Object Relational Mapper) which allow to manipulate a database in a portable and efficient manner.

This guide shows how to use Entity Framework Core in a Xamarin Forms app to persist data on the device.

## Introduction

Entity Framework Core is defined by Microsoft as:

> Entity Framework (EF) Core is a lightweight, extensible, and cross-platform version of the popular Entity Framework data access technology.

In addition to that, version 2.0 of EF Core is compatible with [Xamarin platforms and .Net standard 2.0](https://docs.microsoft.com/fr-fr/ef/core/platforms/). Thanks to that, we can write a single persistence codebase for both iOS, Android and other Xamarin targets. That's really cool.

If you haven't heard about this technology, I really urge to read about it using the abundant documentation on the internet. This guide focuses only on adding and EF Core database to an existing app. You can get the initial code to follow this article [here](https://github.com/yostane/xamarin_guide/tree/master/PostListDetailsXamarin).

The app consists of master/detail app that shows a list of Post objects fetched from a free JSON API. When the user clicks on an item on the list, its detail view appears. The app uses mvvm and IoC principles. If you are curious about these words, you can follow my [previous guide here](https://codeburst.io/displaying-a-detail-page-in-xamarin-using-mvvm-f3518447db96).

Every time the list view appears, we try to call the JSON API and then replace the result with the obtained data. The current implementation, does not persist on the device the fetched data. Thus, if we start the app in offline mode, no data will be displayed. We'll solve this problem by persisting fetched data using EF Core.

OK, ready ? Go !

## Adding EF Core packages

Before writing any EF Core code, we need to first add two NuGet packages. The first one is logically `Microsoft.EntityFrameworkCore`, and second one is `Microsoft.EntityFrameworkCore.Sqlite`. The first package allows to use the EF Core API while the second allows to an SQLite database with EFCore. SQLite is a database technology that is used on mobile platforms (iOS, Android, UWP) and is also available on PC and macOS. So, in the context of a Xamarin.Forms app, it is natural to use SQLite. `Microsoft.EntityFrameworkCore.Sqlite` is called a database provider for SQLite and is officially maintained by the [Entity Framework Core project](https://github.com/aspnet/EntityFrameworkCore). Which is another reason to not refrain from using it !.

Once you have added these two packages and accepted the different popups, you should have a list of packages similar to the illustration below.

![The installed packages](./assets/efcore_packages.png)

we can move on to defining the ORM mapping (spoil, it is easy).

## Defining the ORM Mapping

The ORM marring consists of defining how the objects are stored in the database. Usually, we define the table name, the column name of each property as well as its constraints. Of course, the most important constraint is the **primary key** (PK) because relational databases rely mainly on the existence of PK.

EF Core allows to specify using _annotations_ or by code thanks to the Fluent API. I prefer using the former because we write less code and EF Core also assumes a lot of things for us. In fact, adding one single annotation is sometimes sufficient and we are not required to add annotations for each property. That's exactly what we are going to do in here. Please open the `Post` class which the class that we want to persist. It should look like this:

```cs
public class Post
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("userId")]
    public int UserId { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("body")]
    public string Body { get; set; }

    public string ImageUrl { get; set; }
}
```

As you can see, there already annotations used by the JSON parser. Don't worry, we can mix those with EF Core. The annotation that we want to add is `[Key]` to the `Id` property which qualifies it as the primary key in the corresponding table. Please add that annotation as follows:

```cs
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RandomListXamarin.Model
{

    public class Post
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        public string ImageUrl { get; set; }
    }
}
```

That's it ! This single annotation is sufficient for EF Core to work with the `Post` class. The table name and column are automatically deduced from the code by EF Core. You can of course be more specific and add annotations as documented [here](https://docs.microsoft.com/en-us/ef/core/modeling/relational/) or [here](https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes).

Since ORM mapping is done, we can create the database context in the next step.

## The DbContext

The DbContext (abbreviation of Database Context) is the class responsible for interacting with persisted objects. In other words, we will need to use a DbContext in order to create, read, update or delete `Post` objects in / from the database.

Here is the workflow of using a DbContext

1.  Create a subclass of DbContext which defines the classes that we want to persist and. We will call it `PostDatabaseContext`.
2.  Instantiate a `PostDatabaseContext`, initialize it, and use it to query or update the the obtained object.
3.  If modifications are made (add, update, delete) call `context.SaveChanges()` to commit changes.
4.  Dispose the context instance when we do not want to use it.

Let's start by creating a subclass of `DbContext` called `PostDatabaseContext`. There, we'll first define the SQLite database path by overriding the `OnConfiguring` method and constructing a path in the data folder of the app depending on the system.

```cs
private const string databaseName = "database.db";
protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
    String databasePath = "";
    switch (Device.RuntimePlatform) {
        case Device.iOS:
            SQLitePCL.Batteries_V2.Init ();
            databasePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);;
            break;
        case Device.Android:
            databasePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), databaseName);
            break;
        default:
            throw new NotImplementedException ("Platform not supported");
    }
    // Specify that we will use sqlite and the path of the database here
    optionsBuilder.UseSqlite ($"Filename={databasePath}");
}
```

The `databasePath` value is not the same wether we are on iOS or Android, because the data folder is different depending on the OS. Next, we need to define the tables that the `PostDatabaseContext` manages. We simply do that by defining properties of type `DbSet<class>`. Here is the line that defines a DbSet for the Post class:

```cs
public DbSet<Post> Posts { get; set; }
```

Here is the the full code of the class:

```cs
public class PostDatabaseContext : DbContext {
    /// <summary>
    /// Manipulate the posts table
    /// </summary>
    /// <value>The property that allows to access the Posts table</value>
    public DbSet<Post> Posts { get; set; }

    private const string databaseName = "database.db";
    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
        String databasePath = "";
        switch (Device.RuntimePlatform) {
            case Device.iOS:
                SQLitePCL.Batteries_V2.Init ();
                databasePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);;
                break;
            case Device.Android:
                databasePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), databaseName);
                break;
            default:
                throw new NotImplementedException ("Platform not supported");
        }
        // Specify that we will use sqlite and the path of the database here
        optionsBuilder.UseSqlite ($"Filename={databasePath}");
    }
}
```

The `PostDatabaseContext` is now complete. The next task consists in creating a database helper class.

## Adding a database helper

I like defining a database helper class - that I'll call `PostDatabaseHelper` - to complement the DbContext subclass. The helper is responsible for performing the actual database operations using the context and also handles the context object instance. There are many strategies for handling the lifecycle of a context object. I prefer to create a context when I need one and then dispose it at the end of the operation. This is easily possible thanks to the `using` keyword.

```cs
using (var context = CrateContext())
{
    //perform any operations on the context here
    //The context will be automatically disposed of at the end of the block
}
```

Where `CrateContext` is a method of the helper that creates a context object. In order for for the `PostDatabaseHelper` to be able to create context objects when needed. I think of two clean ways to do it: using generics or using a factory method. I'll go with the generics approach but you are free to use your own technique. Thus, here is the definition the `CrateContext` method.

```cs
public class PostDatabaseHelper<T> where T : PostDatabaseContext
{

    protected PostDatabaseContext CrateContext()
    {
        PostDatabaseContext postDatabaseContext = (T)Activator.CreateInstance(typeof(T));
        postDatabaseContext.Database.EnsureCreated();
        postDatabaseContext.Database.Migrate();
        return postDatabaseContext;
    }
}
```

Calling `(T)Activator.CreateInstance(typeof(T))` creates an instance of Type `T` where is a subclass of or a `PostDatabaseContext`. The next two class `postDatabaseContext.Database.EnsureCreated();` and `postDatabaseContext.Database.Migrate();` ensure that we have a database that is created and is on the latest Schema version. More information about migrating database and EF Core can be found [here](https://medium.com/@yostane/entity-framework-core-and-sqlite-database-migration-using-vs2017-macos-28812c64e7ef).

We can now interact with the database. Let's start by a simple `getPostsAsync` method that loads persisted posts from the database and sorts by id in descending order.

```cs
public async Task<List<Post>> getPostsAsync()
{
    using (var context = CrateContext())
    {
        //We use OrderByDescending because Posts are generally displayed from most recent to oldest
        return await context.Posts
                            .AsNoTracking()
                            .OrderByDescending(post => post.Id)
                            .ToListAsync();
    }
}
```

The call to `AsNoTracking()` is an optimization that I like to not need loaded objects to be linked with context. So, the returned posts are normal objects which are not tracked by EF Core.

But before getting posts, we need to add them to the database. Here is function that adds only new posts to the database.

```cs
public async Task AddOrUpdatePostsAsync(List<Post> posts)
{
    using (var context = CrateContext())
    {
        // add posts that do not exist in the database
        var newPosts = posts.Where(
            post => context.Posts.Any(dbPost => dbPost.Id == post.Id) == false
        );
        await context.Posts.AddRangeAsync(newPosts);
        await context.SaveChangesAsync();
    }
}
```

In this snippet, we are not considering the case where posts were updated on the server side. So, depending on your needs, you should also consider adding an update date filed and update persisted posts that do not have the same date as the server.

## Loading ListView Data from the database

The current loading algorithm

1.  Fetch list of Posts
2.  Clear the Posts ObservableCollection
3.  Add all the fetched posts

As you may note, this is a bit ugly and not optimal. Especially, cleaning the observable collection every time. In fact, since it is a bound collection, any modification will impact the UI. You may have noted a strange behavior with when you go back from a detail page to the posts page; some items disappear and reappear. This is not magic and it's just a reflection of the cleaning and adding actions.

An optimal use of an ObservableCollection is to add or remove items only when necessary. In addition, if an item need be updated, we must not replace the old item with the updated one, this may cause the display to remove the item and replace it with a the updated one. Instead, we must update the fields of the outdated item if the ObservableCollection in order to keep the display in good shape. So, if we want to update the ObservableCollection with the same items, we should do simply nothing.

With all that knowledge in mind, here is my new way of loading posts:

1.  Fetch list of Posts if possible and update the _Posts_ table if we got results from the server
2.  Load the all the posts from the database
3.  Update the observable collection only when necessary

please modify the `UpdatePostsAsync` of the `ItemListViewModel` as follows.

```cs
public async Task UpdatePostsAsync()
{
    try
    {
        //Update from backend if possible and update database
        var newPosts = await JsonPlaceholderHelper.GetPostsAsync();
        await App.Locator.PostDatabaseHelper.AddOrUpdatePostsAsync(newPosts);
    }
    catch (HttpRequestException e)
    {
        System.Diagnostics.Debug.WriteLine($"HTTP request exception {e.Message}");
    }
    //Always dispay what's in the database
    var databasePosts = await App.Locator.PostDatabaseHelper.getPostsAsync();

    //The following algorithm allows to replace only the items that changed in the observable collection
    for (int i = 0; i < databasePosts.Count; i += 1)
    {
        if (Posts.Count <= i)
        {
            Posts.Add(databasePosts[i]);
        }
        else if (databasePosts[i].Id != Posts[i].Id)
        {
            Posts[i] = databasePosts[i];
        }
    }
    //delete remaining items in the Posts collection
    for (int i = databasePosts.Count; i < Posts.Count; i += 1)
    {
        Posts.RemoveAt(i);
    }
}
```

Please run the final app and navigate between pages. The app works just as before with two nice additions. Firsty, the list reloads without moving cells. Secondly, we can use the app even in plane mode !. That's some cool features.

![Demo](./assets/demo.gif)

Now's the time to conclude unfortunately ):.

## Conclusion

This article showed how easy it is to add EF Core to a Xamarin project. Thanks to that, all database code can shared across platforms. We also saw how to use the database to cache data and

The source code is available on [GitHub as usual](https://github.com/yostane/xamarin_guide/tree/master/EntityFrameworkXamarin)

## Links

[Entity Framework Context Lifetime Best Practices](http://blogs.microsoft.co.il/gilf/2010/02/07/entity-framework-context-lifetime-best-practices/)

[Platform Specific Code In SAP And PCL](https://www.c-sharpcorner.com/article/platform-specific-code-in-sap-and-pcl/)

[Entity Framework Core with Xamarin.Forms](https://xamarinhelp.com/entity-framework-core-xamarin-forms/)

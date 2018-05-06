# Persistence in Xamarin using Entity Framework Core

Thanks to .Net Standard 2 and .Net Core, we can take advantage of the astonishing potential of [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/). It is an ORM (Object Relational Mapper) which allow to manipulate a database in a portable and efficient manner.

This guide shows how to use Entity Framework Core in a Xamarin Forms app.

## Introduction

Entity Framework Core is defined by Microsoft as:

> Entity Framework (EF) Core is a lightweight, extensible, and cross-platform version of the popular Entity Framework data access technology.

In addition to that, version 2.0 of EF Core is compatible with [Xamarin platforms and .Net standard 2.0](https://docs.microsoft.com/fr-fr/ef/core/platforms/). Thanks to that, we can write a single persitatnce codebase for both iOS, Android and other Xamarin targets. That's really cool.

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

That's it ! This single annotation is sufficient for EF Core to work with the `Post` class. The table name and column are automatically deduced from the code by EF Core. You can of course be more specific and add annotation from the vast list of [available ones](https://docs.microsoft.com/en-us/ef/core/modeling/relational/).

Since ORM mapping is done, we can create the database context in the next step.

## The DbContext

The DbContext (abbreviation of Database Context) is the class responsible for interacting with persisted objects. In other words, we will need to use a DbContext in order to create, read, update or delete `Post` objects in / from the database.

Here is the workflow of using a DbContext

1.  Create a subclass of DbContext which defines the classes that we want to persist and. We will call it `PostDatabaseContext`.
2.  Instantiate a `PostDatabaseContext`, initialize it, and use it to query or update the the obtained object
3.  If modifications are made (add, update, delete) call `context.SaveChanges()` to commit changes
4.  Dispose the context instance when we do not want to use it

There are many strategies for handling the lifecycle of a context object. I prefer to create a context when I need one and then dispose it at the end of the operation. This is easily possible thanks to the `using` keyword.

```cs
using (var context = CrateContext())
{
    //perform any operations on the context here
    //The context will be automatically disposed of at the end of the block
}
```

## Adding a database helper

## Loading ListView Data from the database

## Conclusion

## Links

http://blogs.microsoft.co.il/gilf/2010/02/07/entity-framework-context-lifetime-best-practices/

https://www.c-sharpcorner.com/article/platform-specific-code-in-sap-and-pcl/

https://xamarinhelp.com/entity-framework-core-xamarin-forms/

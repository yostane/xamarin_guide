# Master detail navigation with Xamarin

In the previous guide, we developed a Xamarin that displays on a list data fetched from the internet. In this guide, we go further by showing a detail view when we tap on an item. We first create the new view without complying to mvmm. Then, we will try to organize the code with respect to the mvvm pattern. Let's begin.

## Adding the PostDetail page

Adding the detail page without considering MVVM is the easier and straightforward way. First, create the `PostDetailViewModel` class as well as the `PostDetail.xaml` page it's code-behind. The view model defines a property of type `Post` which is bound to the xaml view. The code-behind takes a parameter of type of type `Post`and passes it to the view model. Here is the code for each file:

PostDetailViewModel.cs

```cs
public class PostDetailViewModel : BaseViewModel
{
    private Post post;
    public Post Post
    {
        get => post;
        set => SetProperty(ref post, value);
    }

    public PostDetailViewModel(Post post)
    {
        this.Post = post;
    }
}
```

PostDetail.xaml

```xml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
     x:Class="PostListDetailsXamarin.Views.PostDetail" Title="Post detail">
    <ContentPage.Content>
        <StackLayout Orientation="Vertical" Padding="5,5,5,5">
            <Label TextColor="Gray">Title: </Label>
            <Label Text="{x:Binding Post.Title}" />
            <Label TextColor="Gray">Body: </Label>
            <Label Text="{x:Binding Post.Body}" />
            <Image Source="{x:Binding Post.ImageUrl}" WidthRequest="70"
                 HeightRequest="70" />
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

PostDetail.xaml.cs

```cs
public partial class PostDetail : ContentPage
{
    PostDetailViewModel postDetailViewModel;

    public PostDetail()
    {
        InitializeComponent();
    }

    public PostDetail(Post post)
    {
        InitializeComponent();
        postDetailViewModel = new PostDetailViewModel(post);
        this.BindingContext = postDetailViewModel;
    }
}
```

In order to display this new view when the user taps the view, we will handle the onTap event of the `ListView`. Please add the `ItemSelected` attribute to the `ListView` in ItemListPage.xaml.

```xml
<ListView ItemsSource="{x:Binding Posts}"
        SelectedItem="{x:Binding SelectPost}"
        VerticalOptions="FillAndExpand" CachingStrategy="RecycleElement"
        ItemSelected="Handle_ItemSelected">
```

In this code snippet, the `Handle_ItemSelected` function handles the `ItemSelected` event. Here is the implementation of that function:

```cs
async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
{
    var postDetailPage = new PostDetail(e.SelectedItem as Post);
    await Navigation.PushAsync(postDetailPage);
}
```

What we did here is that we ask the `Navigation` property of the `ContentPage` class to navigate to a new page. This new page is an instance of `PostDetail` class that takes the select post as a parameter.

Launch the app and tap on an item in the list. The detail page of that item should appear.

![spoiler](./assets/post-detail.png)

Here, we did it ..., or not. In fact, the code not really follow mvvm _principles_. Here are some of the **sins** of this code:

* The tap event is handled by the view instead of the view model through a command.
* Both the pages instantiate the view models instead of using an IoC container.

In the next section, We'll try to address these points.

## Handling the tap event using a command

A command is a mechanism that allows to handle events in the view model. It works by binding an event in the control to a command in the view model. In order for this to work we need two elements.

* The control must accepts a command property for the event that we want to handle
* The view model must provide a public command that handles the event
* The command in view model must be bound to the control

The second and third conditions are very easy to satisfy, because it is really to define a command and to bind it. However, there is one little gotcha. The ListView control does not accept natively a command property for the tapped event. Since we are not going to [buy a richer ListView control](https://www.telerik.com/purchase/xamarin-ui), we need to find another cue. Hopefully, there is a quite powerful technique in Xamarin that allows to exactly do that. It is called **behaviors**.

## Making the app more MVVM compliant

Install extensions

* MVVLlight
* Navigation service for Xamarin Forms

## Links

[A simple Navigation Service for Xamarin.Forms](https://mallibone.com/post/a-simple-navigation-service-for-xamarinforms?mode=edit)

[Navigation using MVVM Light](https://wolfprogrammer.com/2016/07/22/navigation-using-mvvm-light/)

[Dependency injection with Autofac and MVVM Light in Xamarin](https://www.chipsncookies.com/2016/dependency-injection-with-autofac-and-mvvm-light-in-xamarin/)

[https://docs.microsoft.com/fr-fr/xamarin/xamarin-forms/app-fundamentals/navigation/hierarchical](https://docs.microsoft.com/fr-fr/xamarin/xamarin-forms/app-fundamentals/navigation/hierarchical)

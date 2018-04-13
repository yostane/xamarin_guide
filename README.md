# Xamarin guide

This article will guide through the steps of creating a typical list / detail app using Xamarin Forms. The advantage of using Xamarin Forms is that we will obtain a native Android and iOS apps using a single C# / Xaml codebase.

I am using macOS and Visual Studio Community for macOS but you can also follow the guide using Visual Studio on windows.

Let's start by the beginning of all things; creating the project.

## Creating the project

Open Visual Studio and create a new Project -> Choose Blank App -> And the click . You will a minimal solution that contains three minimal projects (as usual for Xamarin projects :smile: )

As a reminder, we are creating a Xamarin forms app that fetches a list of items from the internet and shows in on a scrollable list. Each row of the list will display an item with some text content and a thumbnail.

We are not going to developer neither the JSON API nor the thumbnail server. Instead, we'll use a free JSON generator for developers [https://jsonplaceholder.typicode.com](https://jsonplaceholder.typicode.com/) and a free image generator [https://picsum.photos](https://picsum.photos/).

Before Starting to code, let's create some folders to organize our code. There wiill 4 main folders: Views, ViewModels, Models and Helpers.

## Fetching 

Ok let's dive in to the code right now and create the item list view.

## The ItemListView
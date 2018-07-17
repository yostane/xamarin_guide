# Getting started with Xamarin development (as of June 2018)

This article aims to provide a state of the art about Xamarin and .Net. I hope reading the following text will give you better understanding and will allow to start developing Xamarin apps more efficiently.

We start by defining what is Xamarin.

## What is Xamarin

Xamarin is a framework that allows to develop native apps for multiple platforms using a single **C#** codebase and **.Net**. Thanks to that, a lot of common code can be shared and reused, allowing to reduce the codebase drastically. This gain is not possible if we have used platform specific frameworks and SDKs (such as Xcode or Android Studio) because it would require to write the whole app for each targeted platform.

Thus, Xamarin is particularly fitting when we develop multiplatofm apps.

Before diving more into Xamarin, let's take a step back and look at its core: the .Net framework.

### The .Net ecosystem: .Net Framework, .Net Standard and Xamarin

.Net is a set of APIs created and developed by Microsoft. It is similar to the Java SE API (without the graphic parts) but much better :smile:. Since .Net is just a specification, many implementations are available. The most famous one is the .Net Framework on Windows which is developed by Microsoft itself. Mono is another implementation for the Linux platform. More recently, Microsoft released **.Net Core** which is a multi-platform implementation available on windows, Linux, macOS and **even on Xamarin**.

Xamarin also implements the .Net Framework (Xamarin was based on Mono) and supports many more platforms through these frameworks: Xamarin.iOS, and Xamarin.Android, Xamarin.Mac and Xamarin.Forms.

One problem that was confusing with .Net is that there was not a standard set of APIs across platforms. In fact, Mono and the .Net Framework were not totally compatible. To address that, Microsoft released **.Net Standard** which specifies a set of APIs that an implantation should provide. Each [.Net implementation](https://docs.microsoft.com/fr-fr/dotnet/standard/net-standard) must now mention the version of .Net standard that it supports. For example, .Net Core 2, .Net Framework 4.6.1 and Mono 5.4 support .Net standard 2.0 (which is the latest specification to date).

_As a side note, before .Net standard, Microsoft tried to unify APIs using PCLs but it was not really successful._

To synthesize, .Net standard is a set of APIs for .Net and Xamarin - among other implementations - supports .Net standard 2.0.

The drawback of .Net core compared to JavaSE is that it does not provide a standard UI library (which is very hard to achieve in practice). The next section gives the current status.

### What about the UI

As opposed to .Net standard, there is a different UI library on each platform in the .Net ecosystem and no standard one. In Windows, we find WPF (Windows Presentation Foundation) and WinForms, in Linux and macOS (and also Windows) we have GtkSharp. As you can see, it is starting to get a little messy.

On the Xamarin side, there are frameworks that allow to develop apps with native UI for iOS, Android and Mac, thanks to Xamarin.iOS, Xamarin.Android and Xamarin.Mac. Each of these rely on the native SDKs.

Wow, there are lot of libraries here. Hopefully, Xamarin is working on a revolutionary UI framework that can unify UI development in the .Net ecosystem. It is called Xamarin.Forms and the next section talks about it.

### Xamarin.Forms

Xamarin teams have developed Xamarin.Forms which is a library that allows to develop native UI using C# and XAML that is compatible with a lot of platforms.

Xamarin.Forms was originally developed to target iOS, Android and UWP. Hopefully, thanks to its design choices and the efforts of the developers, many other other ones were added later. The main supported platforms currently are: iOS, Android, UWP and macOS. However, Xamarin team is working on supporting [more platforms](https://github.com/xamarin/Xamarin.Forms/wiki/Platform-Support) such as GTK# , WPF and even Tizen !.

Let's breath a little and synthesize what we have seen.

### Synthesis

The following illustration shows an overview of the current .Net ecosystem. We have seen that it has many divergencies across platforms. Hopefully, a unification effort has been made through .Net Standard and Xamarin.Forms. Both try to make UI and non UI code easier to write, to maintain, to share and to reuse.

![.Net ecosystem](assets/ecosysteme_dotnet.svg '.Net ecosystem')

With this information in mind, we can guess that Xamarin can support as many platforms as Xamarin.iOS + Xamarin.Android + Xamarin.Mac + Xamarin.Forms support.

In addition to that, we must not forget that all pure .Net Standard code can be shared with .Net Core, .Net Framework and all other .Net Standard projects. Can you measure all this power :open_mouth:.

The next section gives the focus back to Xamarin and shows how a typical Xamarin project is constructed.

## A typical Xamarin project

As illustrated below, A typical Xamarin solution in Visual Studio contains a platform agnostic project that hosts shared code and UI. We add to that a project for each targeted platform. Each one of these projects references the platform agnostic one and contains only its specific UI, assets and features.

![Project structure](assets/xamarin-solution-structure.svg 'Project structure')

It is possible to share code and the UI thanks to two main components: the .Net framework and Xamarin Forms. Platform specific APIs are available in C# thanks to the bindings provided by Xamarin.

To summarize, a Xamarin project revolves around these parts:

- Shared app logic: thanks to .Net standard and its huge amount of APIs and libraries
- Shared UI code: thanks to Xamarin.Forms
- Platform specific assets, UI and features coded in C#: thanks to the C# bindings to the different native APIs.

Theoretically, this way of doing things allows to target any possible platforms, as long as there are C# bindings available and/or support for Xamarin.Forms.

## Conclusion

We have also seen that Xamarin.Forms allows to develop the UI once for different platforms.

Currently, we see that Xamarin is mostly used to make iOS and Android apps. However, I hope to see Xamarin apps appear on many more platforms.

# Getting started with Xamarin development (as of June 2018)

This article aims to provide a state of the art to correctly get started with Xamarin development.

## What is Xamarin

Xamarin is a framework that allows to develop native apps on multiple platforms using a single C# codebase. Thanks to that, a lot common code can be shared across different platforms. Allowing to reduce the codebase drastically. This gain is not possible if we have used Xcode or Android Studio because we need to write the code using native languages (Kotlin, Swift, etc.).

Before diving into Xamarin, let's take a step back and look at its core: the .Net framework.

### The .Net ecosystem: .Net Framework, .Net Standard and Xamarin

.Net is a set of APIs specified and developed by Microsoft. It is similar to the Java SE API (without the graphic parts) but much better :smile:. Since .Net it is just a specification, many implementations are available. The most famous one is the .Net Framework on Windows which is developed by Microsoft. Mono is another implementation for the Linux platform. More recently, Microsoft released **.Net Core** which is a multi-platform implementation available on windows, Linux, macOS and **even on Xamarin**.

Xamarin also implements the .Net Framework (based on Mono) and supports many more platforms through these frameworks: Xamarin.iOS, and Xamarin.Android, Xamarin.Mac and Xamarin.Forms.

One problem that was confusing with .Net is that there was not a standard set of APIs across platforms. In fact, Mono and the .Net Framework were not totally compatible. To address that, Microsoft developed **.Net Standard** which specifies a set of APIs that an implantation should provide. Each [implementation](https://docs.microsoft.com/fr-fr/dotnet/standard/net-standard) must specify the version of .Net standard that it supports. For example, .Net Core 2, .Net Framework 4.6.1 and Mono 5.4 support .Net standard 2.0 (which is the latest specification to date).

_As a side note, before .Net standard, Microsoft tried to unify APIs using PCLs but it was not really successful._

To synthesize, .Net standard is a set of APIs for .Net implementations and Xamarin - among other implementations - supports .Net standard 2.0.

The drawback of .Net core compared to JavaSE is that it does not support UI libraries (which is very hard to achieve in practice). The next section gives the current status.

### What about the UI

As opposed to .Net standard, there is a different UI library on each platform in the .Net ecosystem . In Windows, we find WPF (Windows Presentation Foundation) and WinForms, in Linux and macOS (and also Windows) we have GtkSharp. As you can see, it is starting to get a little messy.

On the Xamarin side, there frameworks that allow to develop apps with native UI for iOS, Android and Mac, thanks to Xamarin.iOS, Xamarin.Android and Xamarin.Mac. Each of these rely on the native SDKs.

Wow, there are lot of libraries here. Hopefully, Xamarin tries to address this problem with a very powerful and interesting framework Xamarin.Forms.

### Xamarin.Forms

Xamarin teams have developed Xamarin.Forms which is a library that allows to develop native UI using C# and XAML that is compatible with many platforms.

Xamarin.Forms was originally developed to target iOS and Android mobile platforms. Hopefully, thanks to its design choices, many other other ones were added later. The main supported platforms currently are: iOS, Android, UWP and macOS. However, Xamarin team is working on supporting [more platforms](https://github.com/xamarin/Xamarin.Forms/wiki/Platform-Support) such as GTK# , WPF and even Tizen !.

### Synthesis

The following illustration shows an overview of the current .Net ecosystem. We have seen that it has many divergencies across platforms. Hopefully, a unification effort has been made through .Net Standard and Xamarin.Forms. Both try to make UI and non UI code easier to write, to maintain and without rewriting it on different platforms.

![.Net ecosystem](assets/ecosysteme_dotnet.svg '.Net ecosystem')

With this information in mind, we can guess that Xamarin supports as many platforms as Xamarin.iOS + Xamarin.Android + Xamarin.Mac + Xamarin.Forms support.

In addition to that, we must not forget that all pure .Net Standard code can be shared with .Net Core, .Net Framework and all other .Net Standard implementations. Can you measure all this power :open_mouth:.

The next, section shows how a typical Xamarin project is constructed.

## A typical Xamarin project

As illustrated below, A typical Xamarin solution in Visual Studio contains a platform agnostic project that hosts shared code and UI. We add to that a project for each targeted platform. Each one of these projects references the platform agnostic one and contains only its specific UI, assets and features.

![Project structure](assets/xamarin-solution-structure.svg 'Project structure')

It is possible to share code and the UI thanks to two main components: the .Net framework and Xamarin Forms. Platform specific APIs are available in C# thanks to the bindings provided by Xamarin.

To synthesize, a Xamarin project revolves around these parts:

- Shared app logic: thanks to .Net standard and its huge amount of APIs and libraries
- Shared UI code: thanks to Xamarin Forms
- Platform specific assets, UI and features coded in C#: thanks to the C# bindings to the different native APIs.

Theoretically, this way of doing things allows to target any possible platforms, as long as there are C# bindings available. We are going to take a tour of the supported platforms next.

## Conclusion

We have also seen that Xamarin.Forms allows to develop the UI once for different platforms.

Currently, we see that Xamarin is mostly used to make iOS and Android apps. However, I hope to see Xamarin apps appear on many more platforms.

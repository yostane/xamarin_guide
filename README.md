# Getting started with Xamarin development (as of June 2018)

This article aims to provide a state of the art to correctly get started with Xamarin development.

## What is Xamarin

Xamarin is a framework that allows to develop native apps on multiple platforms using a single C# codebase. Thanks to that, common code can be shared across different platforms, which is not possible if we have used Xcode or Android Studio.

## Code sharing and Xamarin Forms

A typical Xamarin generally project (or solution) contains a platform agnostic project that hosts shared code and UI. We add to that a project for each targeted platform. Each one of these projects references the platform agnostic one and contains only its specific UI, assets and features. Platform specific APIs are available in C# thanks to the bindings provided by Xamarin.

It is possible to share code and the UI thanks to two main components: the .Net framework and Xamarin Forms.

### .Net Frameowrk

The .Net Framework is a set of APIs that allow to make console applications. It is similar to the Java SE API (without the graphic parts). Many .Net implementations are available, the most famous one is the .Net Framework on Windows. Mono is another implementation for the Linux platform. Xamarin also has an implementation of the .Net Framework (base on Mono) for each supported platform.

Since the .Net Framework does not specify APIs for making UIs. Xamarin teams have developed Xamarin.Forms which is a library that allows to develop the app UI using C# and XAML.

### Xamarin.Forms

### Synthesis

The following illustration shows a typical Xamarin solution. As we can see, there is a shared project that uses .Net standard, Xamarin.Forms and other .Net libraries.

To synthesize, a Xamarin project revolves around these parts:

- Shared app logic: thanks to .Net standard and its huge amount of APIs and libraries
- Shared UI code: thanks to Xamarin Forms
- Platform specific assets, UI and features coded in C#: thanks to the C# bindings to the different native APIs.

Theoretically, this way of doing things allows to target any possible platforms, as long as there are C# bindings available. We are going to take a tour of the supported platforms next.

## Supported Platforms

Xamarin were originally developed to target mobile platforms and other ones were added later. The main supported platforms currently are: iOS, Android, UWP and macOS. However, Xamarin team is working on supporting [more platforms](https://github.com/xamarin/Xamarin.Forms/wiki/Platform-Support) such as GTK# and WPF.

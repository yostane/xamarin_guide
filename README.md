# Getting started with Xamarin development (as of June 2018)

This article aims to provide a state of the art to correctly get started with Xamarin development.

## What is Xamarin

Xamarin is a framework that allows to develop native apps on multiple platforms using a single C# codebase. Thanks to that, common code can be shared across different platforms, which is not possible if we have used Xcode or Android Studio.

## Code sharing

With respect to code, a Xamarin project can be viewed as a mix of 3 elements:

- Shared app logic: thanks to .Net standard and its huge amount of APIs and libraries
- Shared UI code: thanks to Xamarin Forms
- Platform specific assets, UI and features coded in C#: thanks to the C# bindings to the different native APIs.

Thus, a typical Xamarin generally project (or solution) contains a platform agnostic project that hosts shared code and UI and an additional project for each targeted platform. Each of these project references the platform agnostic one and contains only its specific UI, assets and features.

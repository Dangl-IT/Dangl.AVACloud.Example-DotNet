# Dangl.AVACloud.Example-DotNet

This repository contains example code for the AVACloud .NET Client

This is a _demonstration_ library intended to show the usage of the Dangl.AVACloud .NET Client.  
For a full example of the GAEB & AVA .Net Libraries, you can look here: https://github.com/Dangl-IT/Dangl.AVA.Examples

## Evaluation Packages

This example uses the public client from NuGet, so no setup action is required.

If you have access to an evaluation package, please place them in the folder `local-packages` in the solution root and change the package reference from `Dangl.AVACloud.Client.Public` to `Dangl.AVACloud.Client` to get access to more features.

> If you are already a customer with support contract, please see any of the packages documentation on how to set up the official NuGet feed for **DanglIT** packages.

[Please get in touch with us if you are interested in the libraries](https://www.dangl-it.com/contact/?message=I%27m+interested+in+Dangl.GAEB+%26+Dangl.AVA.+Please+contact+me.).

## Authentication

To access AVACloud, you need to be authenticated. For this, please provide a value for `clientId` and `clientSecret` in the `Program.cs` file of the project. If you don't have a client yet, you need to register as a developer and create one. You can find a step by step guide how to do this here: https://docs.dangl-it.com/Projects/AVACloud/latest/howto/registration/developer_signup.html

## CLI Interface

There are no parameters required for this demo. An example GAEB file named `GAEBXML_EN.X86` is placed in the project folder, and this file is used for the demo.

When running the project, either via `dotnet run` from the CLI in the project repository or just launched via Visual Studio, the conversion process runs automatically.

## AVACloud Key Features

- Can read all GAEB90, GAEB2000 and GAEB XML files. The GAEB library includes a lot of code that can recover from errors that were found in files out in the wild
- Hassle-free import: Just pass the `Stream` of the file to the converter, format detection and error recovery happens automatically
- All libraries are available with both .Net and NETStandard targets, making them usable on virtually all platforms (for example on Windows, Linux, Mac and Xamarin)
- **Dangl.GAEB** provides a native interface to all features of GAEB files, allowing native operation directly on the GAEB file
- **Dangl.AVA** offers a unified data model that can be bi-directionally imported or exported to via **Dangl.AVA.Converter** between GAEB, Excel and Json
- Advanced heuristics allow the preservation of most information even when converting to an earlier version of the GAEB standard
- Complete `INotifyPropertyChanged` support in **Dangl.AVA** and event driven messaging makes it directly usable in front end applications - Set the price of an item and the whole bill of quantity is automatically updated
- Over **175.000** tests are run automatically on every commit. The tests cover 7 frameworks (both full .Net and .Net Core) and over 200 GAEB files

### Supported Formats

![AVACloud Features](./img/AVACloud%20Diagram%20EN.png)

**... and many more!**

### UI Components

Easy integration with prebuilt UI components is possible within minutes:

- Either by using our Angular specific `@dangl/angular-ava` package: <https://www.npmjs.com/package/@dangl/angular-ava>
- Or with our framework agnostict Html web component implementation that run anywhere, either in web apps or locally in a web view: <https://www.npmjs.com/package/@dangl/web-components-ava>

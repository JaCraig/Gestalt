# <img src="https://jacraig.github.io/Gestalt/images/Icon.png" style="height:25px" alt="Gestalt Icon" /> Gestalt

[![.NET Publish](https://github.com/JaCraig/Gestalt/actions/workflows/dotnet-publish.yml/badge.svg)](https://github.com/JaCraig/Gestalt/actions/workflows/dotnet-publish.yml) [![Coverage Status](https://coveralls.io/repos/github/JaCraig/Gestalt/badge.svg?branch=main)](https://coveralls.io/github/JaCraig/Gestalt?branch=main)

Gestalt is a C# library designed to facilitate the development of modular applications. It allows developers to create reusable modules that can be easily integrated into various applications, enhancing code reusability and maintainability.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Creating Modules](#creating-modules)
  - [Using Modules](#using-modules)
- [Contributing](#contributing)
- [License](#license)

## Introduction

Modern software development often requires building applications with modular architectures to promote reusability and scalability. Gestalt provides a framework for organizing application functionality into cohesive modules, encapsulating logic and services that can be easily shared across different projects.

## Features

- **Modular Architecture:** Define modules encapsulating configuration, services, and lifecycle events.
- **Configuration Management:** Easily configure application settings using built-in configuration providers.
- **Dependency Injection:** Utilize dependency injection for managing dependencies and services.
- **Lifecycle Management:** Implement lifecycle events such as application start, stop, and shutdown.

## Getting Started

### Installation

Gestalt can be installed via NuGet Package Manager:

```bash
dotnet add package Gestalt.Core
```

Or for framework specific functionality, install the appropriate package:

```bash
# For ASP.NET base functionality
dotnet add package Gestalt.AspNet
# For ASP.NET MVC functionality
dotnet add package Gestalt.AspNet.MVC
# For ASP.NET Controllers functionality (without views)
dotnet add package Gestalt.AspNet.Controllers
# For ASP.NET SignalR functionality
dotnet add package Gestalt.AspNet.SignalR
# For ASP.NET Razor Pages functionality
dotnet add package Gestalt.AspNet.RazorPages
# For console applications
dotnet add package Gestalt.Console
```

### Creating Modules

To create a module, follow these steps:

1. Define a class that inherits from one of the *ModuleBaseClasses or implements one of the IApplicationModule interfaces (IMvcModule, ISignalRModule, IRazorPagesModule, etc).

2. Override methods for configuring settings, services, and lifecycle events.

3. Implement module-specific functionality within the class.

A basic example of a module class is shown below:

```csharp
/// <summary>
/// This is an example of a basic module that configures the application with the usual default settings when creating a new web application.
/// It implements the MvcModuleBaseClass which simplifies the process of creating a module that needs to modify MVC settings.
/// However, you can implement the IMvcModule interface directly if you prefer.
/// </summary>
public class BasicModule : MvcModuleBaseClass<BasicModule>
{
    /// <summary>
    /// This is called to configure the IApplicationBuilder object. Since this is an MVC app, that would be the WebApplication object.
    /// </summary>
    /// <param name="applicationBuilder">The application builder object.</param>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="environment">The host environment object.</param>
    /// <returns>The application builder object should be returned.</returns>
    public override IApplicationBuilder? ConfigureApplication(IApplicationBuilder? applicationBuilder, IConfiguration? configuration, IHostEnvironment? environment)
    {
        if (applicationBuilder is null)
            return applicationBuilder;

        // Configure the HTTP request pipeline.
        if (environment?.IsDevelopment() == false)
        {
            // This is the default exception handler for the application when in production.
            _ = applicationBuilder.UseExceptionHandler("/Home/Error");

            // We will add a strict transport security header to the response.
            _ = applicationBuilder.UseHsts();
        }

        // This will redirect HTTP requests to HTTPS.
        _ = applicationBuilder.UseHttpsRedirection();
        // And let's serve static files.
        _ = applicationBuilder.UseStaticFiles();
        // We will also add authorization to the application.
        _ = applicationBuilder.UseAuthorization();
        // And lastly, we will return the application builder.
        return applicationBuilder;
    }

    /// <summary>
    /// This is called to configure the MVC options. We will just return the options object as is.
    /// </summary>
    /// <param name="mVCBuilder">The MVC builder object.</param>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="environment">The host environment object.</param>
    /// <returns>The MVC builder object.</returns>
    public override IMvcBuilder? ConfigureMVC(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment)
    {
        return mVCBuilder;
    }

    /// <summary>
    /// This is called to configure our endpoint routes. For our example, we will just use the default route.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="environment">The host environment object.</param>
    /// <returns>The endpoint route builder.</returns>
    public override IEndpointRouteBuilder? ConfigureRoutes(IEndpointRouteBuilder? endpoints, IConfiguration? configuration, IHostEnvironment? environment)
    {
        if (endpoints is null)
            return endpoints;

        // Map the default route.
        endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        return endpoints;
    }
}
```

### Using Modules

To have your application start using modules, you just need to call UseGestalt on the application builder:

```csharp
// Program.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.UseGestalt<ExampleModule>();
        app.Run();
    }
}
```
For more advanced usage, refer to the [documentation](https://jacraig.github.io/Gestalt/articles/intro.html).

## Contributing
Contributions to Gestalt are welcome! If you encounter any issues or have suggestions for improvements, please feel free to open an issue or submit a pull request. Before contributing, please review the [contribution guidelines](https://github.com/JaCraig/Gestalt/blob/main/CONTRIBUTING.md).

## License

Gestalt is licensed under the [Apache 2.0 License](https://github.com/JaCraig/Gestalt/blob/main/LICENSE).
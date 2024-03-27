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
dotnet add package Gestalt
```

### Creating Modules

To create a module, follow these steps:

1. Define a class that inherits from one of the *ModuleBaseClasses or implements one of the IApplicationModule interfaces (IMvcModule, ISignalRModule, IRazorPagesModule, etc).

2. Override methods for configuring settings, services, and lifecycle events.

3. Implement module-specific functionality within the class.

The most basic example of a module class is shown below:

```csharp
// ExampleModule.cs
public class ExampleModule : ApplicationModuleBaseClass<ExampleModule>
{
    // Override configuration, service registration, and lifecycle methods here
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

Gestalt is licensed under the [Appache 2.0 License](https://github.com/JaCraig/Gestalt/blob/main/LICENSE).
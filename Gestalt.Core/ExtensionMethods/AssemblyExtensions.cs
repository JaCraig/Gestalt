﻿using BigBook;
using Gestalt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Gestalt.Core.ExtensionMethods
{
    /// <summary>
    /// Assembly Extension Methods
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Finds the assemblies in the application directory.
        /// </summary>
        /// <param name="entryAssembly">The entry assembly.</param>
        /// <returns>The list of assemblies in the same directory as the entry assembly.</returns>
        public static Assembly[] FindAssemblies(this Assembly? entryAssembly)
        {
            if (entryAssembly is null)
                return Array.Empty<Assembly>();

            var AssembliesFound = new HashSet<Assembly>
                {
                    entryAssembly
                };
            var Temp = entryAssembly.Location;
            var DirectoryPath = Path.GetDirectoryName(Temp);
            if (string.IsNullOrEmpty(DirectoryPath))
                return AssembliesFound.ToArray();
            foreach (var TempAssembly in new DirectoryInfo(DirectoryPath)?.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly) ?? Enumerable.Empty<FileInfo>())
            {
                try
                {
                    AssembliesFound.Add(Assembly.LoadFrom(TempAssembly.FullName));
                }
                catch { }
            }
            return AssembliesFound.ToArray();
        }

        /// <summary>
        /// Finds the frameworks in a list of assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to search.</param>
        /// <returns>The application frameworks.</returns>
        public static IApplicationFramework[] FindFrameworks(this Assembly?[]? assemblies)
        {
            if (assemblies is null || assemblies.Length == 0)
                return Array.Empty<IApplicationFramework>();

            var ReturnValue = new HashSet<IApplicationFramework>();
            for (int I = 0, AssembliesLength = assemblies.Length; I < AssembliesLength; I++)
            {
                Assembly? TempAssembly = assemblies[I];
                if (TempAssembly is null)
                    continue;
                try
                {
                    var ModuleTypes = TempAssembly.GetTypes()
                        .Where(x => typeof(IApplicationFramework).IsAssignableFrom(x) && x.GetConstructor(Type.EmptyTypes) != null);
                    ReturnValue.Add(ModuleTypes.Create<IApplicationFramework>());
                }
                catch { }
            }
            return ReturnValue.OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        /// Finds the modules in a list of assemblies.
        /// </summary>
        /// <returns>The application modules.</returns>
        public static IApplicationModule[] FindModules(this Assembly?[]? assemblies)
        {
            if (assemblies is null || assemblies.Length == 0)
                return Array.Empty<IApplicationModule>();

            var ReturnValue = new HashSet<IApplicationModule>();
            for (int I = 0, AssembliesLength = assemblies.Length; I < AssembliesLength; I++)
            {
                Assembly? TempAssembly = assemblies[I];
                if (TempAssembly is null)
                    continue;
                try
                {
                    var ModuleTypes = TempAssembly.GetTypes()
                        .Where(x => typeof(IApplicationModule).IsAssignableFrom(x) && x.GetConstructor(Type.EmptyTypes) != null);
                    ReturnValue.Add(ModuleTypes.Create<IApplicationModule>());
                }
                catch { }
            }
            return ReturnValue.OrderBy(x => x.Order).ToArray();
        }
    }
}
using BigBook;
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

            var AssembliesFound = new List<Assembly>
            {
                entryAssembly
            };
            var Temp = typeof(AssemblyExtensions).Assembly.Location;
            foreach (FileInfo? TempAssembly in new FileInfo(Temp).Directory?.EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly) ?? Array.Empty<FileInfo>())
            {
                try
                {
                    AssembliesFound.Add(Assembly.Load(AssemblyName.GetAssemblyName(TempAssembly.FullName)));
                }
                catch { }
            }
            return AssembliesFound.Distinct().ToArray();
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

            var ReturnValue = new List<IApplicationFramework>();
            for (int I = 0, AssembliesLength = assemblies.Length; I < AssembliesLength; I++)
            {
                Assembly? TempAssembly = assemblies[I];
                if (TempAssembly is null)
                    continue;
                try
                {
                    IEnumerable<TypeInfo> ModuleTypes = TempAssembly
                        .DefinedTypes
                        .Where(x => x.Is<IApplicationFramework>() && x.HasDefaultConstructor());
                    _ = ReturnValue.Add(ModuleTypes.Create<IApplicationFramework>());
                }
                catch { }
            }
            return ReturnValue.Distinct().OrderBy(x => x.Order).ToArray();
        }

        /// <summary>
        /// Finds the modules in a list of assemblies.
        /// </summary>
        /// <returns>The application modules.</returns>
        public static IApplicationModule[] FindModules(this Assembly?[]? assemblies)
        {
            if (assemblies is null || assemblies.Length == 0)
                return Array.Empty<IApplicationModule>();

            var ReturnValue = new List<IApplicationModule>();
            for (int I = 0, AssembliesLength = assemblies.Length; I < AssembliesLength; I++)
            {
                Assembly? TempAssembly = assemblies[I];
                if (TempAssembly is null)
                    continue;
                try
                {
                    IEnumerable<TypeInfo> ModuleTypes = TempAssembly.DefinedTypes
                                                  .Where(x => x.Is<IApplicationModule>()
                                                           && x.HasDefaultConstructor());
                    _ = ReturnValue.Add(ModuleTypes.Create<IApplicationModule>());
                }
                catch { }
            }
            return ReturnValue.Distinct().OrderBy(x => x.Order).ToArray();
        }
    }
}
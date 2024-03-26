using BenchmarkDotNet.Attributes;
using BigBook;
using Gestalt.Core.ExtensionMethods;
using Gestalt.Core.Interfaces;
using System.Reflection;

namespace Gestalt.SpeedTests.Core
{
    public static class TestExtensions
    {
        public static Assembly[] FindAssembliesNew(this Assembly? entryAssembly)
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
        public static IApplicationFramework[] FindFrameworksNew(this Assembly?[]? assemblies)
        {
            if (assemblies is null || assemblies.Length == 0)
                return Array.Empty<IApplicationFramework>();

            var ReturnValue = new HashSet<IApplicationFramework>();
            var ApplicationFrameworkType = typeof(IApplicationFramework);
            for (int I = 0, AssembliesLength = assemblies.Length; I < AssembliesLength; I++)
            {
                Assembly? TempAssembly = assemblies[I];
                if (TempAssembly is null)
                    continue;
                try
                {
                    var ModuleTypes = TempAssembly.GetTypes()
                        .Where(x => ApplicationFrameworkType.IsAssignableFrom(x) && x.GetConstructor(Type.EmptyTypes) != null);
                    ReturnValue.Add(ModuleTypes.Create<IApplicationFramework>());
                }
                catch { }
            }
            return ReturnValue.OrderBy(x => x.Order).ToArray();
        }
    }

    [RankColumn, MemoryDiagnoser]
    public class AssemblyExtensionsTests
    {
        private Assembly?[] EntryAssembly { get; } = Assembly.GetEntryAssembly().FindAssemblies();

        [Benchmark(Baseline = true)]
        public void CurrentImplementation()
        {
            _ = EntryAssembly.FindFrameworks();
        }

        [Benchmark]
        public void NewImplementation()
        {
            _ = EntryAssembly.FindFrameworksNew();
        }
    }
}
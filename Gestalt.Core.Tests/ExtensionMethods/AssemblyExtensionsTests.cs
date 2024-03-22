using Gestalt.Core.ExtensionMethods;
using Gestalt.Tests.Helpers;
using System;
using System.Reflection;
using Xunit;

namespace Gestalt.Core.Tests.ExtensionMethods
{
    public class AssemblyExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(Core.ExtensionMethods.AssemblyExtensions);

        [Fact]
        public void CanCallFindAssemblies()
        {
            // Arrange
            var EntryAssembly = Assembly.GetAssembly(typeof(string));

            // Act
            Assembly[] Result = EntryAssembly.FindAssemblies();

            // Assert
            Assert.NotNull(Result);
            Assert.True(Result.Length > 1);
            Assert.Equal(EntryAssembly, Result[0]);
        }

        [Fact]
        public void CanCallFindFrameworks()
        {
            // Arrange
            Assembly?[] Assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };

            // Act
            Interfaces.IApplicationFramework[] Result = Assemblies.FindFrameworks();

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanCallFindModules()
        {
            // Arrange
            Assembly?[] Assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };

            // Act
            Interfaces.IApplicationModule[] Result = Assemblies.FindModules();

            // Assert
            Assert.NotNull(Result);
            Assert.Empty(Result);
        }
    }
}
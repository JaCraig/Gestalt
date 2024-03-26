namespace Gestalt.Console.Tests.ExtensionMethods
{
    using Gestalt.Console.ExtensionMethods;
    using Gestalt.Tests.Helpers;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Reflection;
    using Xunit;

    public class HostApplicationBuilderExtensionsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; set; } = typeof(HostApplicationBuilderExtensions);

        [Fact]
        public void CanCallUseGestalt()
        {
            // Arrange
            var App = new HostApplicationBuilder();
            var Args = new[] { "TestValue249179531", "TestValue1954628041", "TestValue1472214480" };
            var Assemblies = new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) };

            // Act
            var Result = App.UseGestalt(Args, Assemblies);

            // Assert
            Assert.NotNull(Result);
            Assert.NotNull(Result.Services);
        }

        [Fact]
        public void CanCallUseGestaltWithNullArgs()
        {
            new HostApplicationBuilder().UseGestalt(default, new[] { Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)), Assembly.GetAssembly(typeof(string)) });
            new HostApplicationBuilder().UseGestalt(default, default);
            new HostApplicationBuilder().UseGestalt(new[] { "TestValue-1", "TestValue-2", "TestValue-3" }, default);
        }
    }
}
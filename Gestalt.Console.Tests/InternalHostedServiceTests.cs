namespace Gestalt.Console.Tests
{
    using Gestalt.Console;
    using Gestalt.Core.Interfaces;
    using Gestalt.Tests.Helpers;
    using NSubstitute;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class InternalHostedServiceTests : TestBaseClass<InternalHostedService>
    {
        public InternalHostedServiceTests()
        {
            _Application = Substitute.For<IApplication>();
            _TestClass = new InternalHostedService(_Application);
        }

        private readonly IApplication _Application;
        private readonly InternalHostedService _TestClass;

        [Fact]
        public async Task CanCallStartAsync()
        {
            // Arrange
            var CancellationToken = System.Threading.CancellationToken.None;

            // Act
            await _TestClass.StartAsync(CancellationToken);
        }

        [Fact]
        public async Task CanCallStopAsync()
        {
            // Arrange
            var CancellationToken = System.Threading.CancellationToken.None;

            // Act
            await _TestClass.StopAsync(CancellationToken);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new InternalHostedService(_Application);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanConstructWithNullApplication()
        {
            _ = new InternalHostedService(default);
        }
    }
}
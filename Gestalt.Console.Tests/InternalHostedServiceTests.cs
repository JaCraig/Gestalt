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

    /// <summary>
    /// Represents a test class for the InternalHostedService class.
    /// </summary>
    public class InternalHostedServiceTests : TestBaseClass<InternalHostedService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalHostedServiceTests"/> class.
        /// </summary>
        public InternalHostedServiceTests()
        {
            _Application = Substitute.For<IApplication>();
            _TestClass = new InternalHostedService(_Application);
        }

        private readonly IApplication _Application;
        private readonly InternalHostedService _TestClass;

        /// <summary>
        /// Tests the <see cref="InternalHostedService.StartAsync(CancellationToken)"/> method.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task CanCallStartAsync()
        {
            // Arrange
            var CancellationToken = System.Threading.CancellationToken.None;

            // Act
            await _TestClass.StartAsync(CancellationToken);
        }

        /// <summary>
        /// Tests the <see cref="InternalHostedService.StopAsync(CancellationToken)"/> method.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Fact]
        public async Task CanCallStopAsync()
        {
            // Arrange
            var CancellationToken = System.Threading.CancellationToken.None;

            // Act
            await _TestClass.StopAsync(CancellationToken);
        }

        /// <summary>
        /// Tests the constructor of the <see cref="InternalHostedService"/> class.
        /// </summary>
        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new InternalHostedService(_Application);

            // Assert
            Assert.NotNull(Instance);
        }

        /// <summary>
        /// Tests the constructor of the <see cref="InternalHostedService"/> class with a null application.
        /// </summary>
        [Fact]
        public void CanConstructWithNullApplication()
        {
            _ = new InternalHostedService(default);
        }
    }
}
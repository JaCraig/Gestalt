using Gestalt.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Gestalt.Console
{
    /// <summary>
    /// Internal hosted service for the console application.
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="InternalHostedService"/> class.</remarks>
    /// <param name="application">Application object</param>
    public class InternalHostedService(IApplication? application) : IHostedService
    {
        /// <summary>
        /// Gets the application.
        /// </summary>
        private IApplication? Application { get; } = application;

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Async task.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Application?.OnStarted();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Async task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Application?.OnStopped();
            return Task.CompletedTask;
        }
    }
}
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Gestalt.Example.SelfContainedModule
{
    /// <summary>
    /// Our custom system status health check. This will check if the application is stopping and return an unhealthy status if it is.
    /// </summary>
    /// <seealso cref="IHealthCheck"/>
    /// <seealso cref="IDisposable"/>
    public class SystemStatusHealthCheck : IHealthCheck, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemStatusHealthCheck"/> class.
        /// </summary>
        /// <param name="hostApplicationLifetime">The host application lifetime.</param>
        public SystemStatusHealthCheck(IHostApplicationLifetime? hostApplicationLifetime)
        {
            if (hostApplicationLifetime is null)
                return;
            HostApplicationLifetime = hostApplicationLifetime;
            CancellationTokenRegistration = HostApplicationLifetime.ApplicationStopping.Register(AppStopping);
        }

        /// <summary>
        /// Gets the cancellation token registration.
        /// </summary>
        /// <value>The cancellation token registration.</value>
        private CancellationTokenRegistration CancellationTokenRegistration { get; set; }

        /// <summary>
        /// Gets the host application lifetime.
        /// </summary>
        /// <value>The host application lifetime.</value>
        private IHostApplicationLifetime? HostApplicationLifetime { get; }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">
        /// A <see cref="T:System.Threading.CancellationToken"/> that can be used to cancel the
        /// health check.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that completes when the health check has
        /// finished, yielding the status of the component being checked.
        /// </returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(CancellationTokenRegistration == default ? HealthCheckResult.Unhealthy("Application stopping") : HealthCheckResult.Healthy("No issues discovered"));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            CancellationTokenRegistration.Dispose();
            CancellationTokenRegistration = default;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Applications the stopping.
        /// </summary>
        private void AppStopping() => Dispose();
    }
}
using Gestalt.Core.BaseClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Gestalt.Console
{
    /// <summary>
    /// Console application class
    /// </summary>
    public class ConsoleApplication : ApplicationBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="env">Host environment</param>
        /// <param name="assemblies">Assemblies</param>
        public ConsoleApplication(IConfiguration? configuration, IHostEnvironment? env, params Assembly?[]? assemblies)
            : base(configuration, env, assemblies)
        {
        }

        /// <summary>
        /// Configures the metrics settings for the application.
        /// </summary>
        /// <param name="metricsBuilder">The metrics builder</param>
        public void ConfigureMetrics(IMetricsBuilder metricsBuilder)
        {
            if (metricsBuilder is null)
                return;

            InternalLogger?.LogInformation("Configuring metrics");

            for (int I = 0, ModulesLength = Modules.Length; I < ModulesLength; I++)
            {
                Core.Interfaces.IApplicationModule Module = Modules[I];
                _ = Module.ConfigureMetrics(metricsBuilder, Configuration, Environment);
            }
        }
    }
}
using Gestalt.ASPNet.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.SignalR.Interfaces
{
    /// <summary>
    /// Module interface for ASP.NET SignalR functionality.
    /// </summary>
    /// <seealso cref="IAspNetModule"/>
    public interface ISignalRModule : IAspNetModule
    {
        /// <summary>
        /// Configures the SignalR framework.
        /// </summary>
        /// <param name="builder">SignalR builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The SignalR builder</returns>
        ISignalRServerBuilder? ConfigureSignalR(ISignalRServerBuilder? builder, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the SignalR options.
        /// </summary>
        /// <param name="options">SignalR options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The SignalR options</returns>
        HubOptions? Options(HubOptions? options, IConfiguration? configuration, IHostEnvironment? environment);
    }
}
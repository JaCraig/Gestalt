using Gestalt.ASPNet.BaseClasses;
using Gestalt.ASPNet.SignalR.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.SignalR.BaseClasses
{
    /// <summary>
    /// SignalR Module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="AspNetModuleBaseClass{TModule}"/>
    /// <seealso cref="ISignalRModule"/>
    public abstract class SignalRModuleBaseClass<TModule> : AspNetModuleBaseClass<TModule>, ISignalRModule
        where TModule : SignalRModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="contentPath">The content path.</param>
        /// <param name="tags">The tags.</param>
        protected SignalRModuleBaseClass(string? name, string? category, string? contentPath, params string[] tags)
            : base(name, category, contentPath, tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected SignalRModuleBaseClass()
        {
        }

        /// <summary>
        /// Configures the SignalR framework.
        /// </summary>
        /// <param name="builder">SignalR builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The SignalR builder</returns>
        public virtual ISignalRServerBuilder? ConfigureSignalR(ISignalRServerBuilder? builder, IConfiguration configuration, IHostEnvironment environment) => builder;

        /// <summary>
        /// Configures the SignalR options.
        /// </summary>
        /// <param name="options">SignalR options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The SignalR options</returns>
        public virtual HubOptions Options(HubOptions options, IConfiguration configuration, IHostEnvironment environment) => options;
    }
}
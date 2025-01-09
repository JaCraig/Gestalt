using Gestalt.ASPNet.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Gestalt.ASPNet.Controllers.Interfaces
{
    /// <summary>
    /// Module interface for ASP.NET MVC controller only functionality.
    /// </summary>
    /// <seealso cref="IAspNetModule"/>
    public interface IControllerModule : IAspNetModule
    {
        /// <summary>
        /// Configures the MVC framework.
        /// </summary>
        /// <param name="mVCBuilder">MVC builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The MVC builder</returns>
        [return: NotNullIfNotNull(nameof(mVCBuilder))]
        IMvcBuilder? ConfigureMVC(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the MVC options.
        /// </summary>
        /// <param name="options">MVC options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The MVC options</returns>
        [return: NotNullIfNotNull(nameof(options))]
        MvcOptions? Options(MvcOptions? options, IConfiguration? configuration, IHostEnvironment? environment);
    }
}
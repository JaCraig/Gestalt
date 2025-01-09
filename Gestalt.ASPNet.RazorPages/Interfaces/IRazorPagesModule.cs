using Gestalt.ASPNet.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Gestalt.ASPNet.RazorPages.Interfaces
{
    /// <summary>
    /// Razor Pages Module Interface
    /// </summary>
    /// <seealso cref="IAspNetModule"/>
    public interface IRazorPagesModule : IAspNetModule
    {
        /// <summary>
        /// Configures the RazorPages framework.
        /// </summary>
        /// <param name="mVCBuilder">RazorPages builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The RazorPages builder</returns>
        [return: NotNullIfNotNull(nameof(mVCBuilder))]
        IMvcBuilder? ConfigureRazorPages(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment);

        /// <summary>
        /// Configures the RazorPages options.
        /// </summary>
        /// <param name="options">RazorPages options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The RazorPages options</returns>
        [return: NotNullIfNotNull(nameof(options))]
        RazorPagesOptions? Options(RazorPagesOptions? options, IConfiguration? configuration, IHostEnvironment? environment);
    }
}
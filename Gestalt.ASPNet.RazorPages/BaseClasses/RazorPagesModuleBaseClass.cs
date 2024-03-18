using Gestalt.ASPNet.BaseClasses;
using Gestalt.ASPNet.RazorPages.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.RazorPages.BaseClasses
{
    /// <summary>
    /// Razor Pages Module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="AspNetModuleBaseClass{TModule}"/>
    /// <seealso cref="IRazorPagesModule"/>
    public abstract class RazorPagesModuleBaseClass<TModule> : AspNetModuleBaseClass<TModule>, IRazorPagesModule
        where TModule : RazorPagesModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RazorPagesModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="contentPath">The content path.</param>
        /// <param name="tags">The tags.</param>
        protected RazorPagesModuleBaseClass(string? name, string? category, string? contentPath, params string[] tags)
            : base(name, category, contentPath, tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RazorPagesModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected RazorPagesModuleBaseClass()
        {
        }

        /// <summary>
        /// Configures the Razor Pages framework.
        /// </summary>
        /// <param name="mVCBuilder">Razor Pages builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The Razor Pages builder</returns>
        public virtual IMvcBuilder? ConfigureRazorPages(IMvcBuilder? mVCBuilder, IConfiguration configuration, IHostEnvironment environment) => mVCBuilder;

        /// <summary>
        /// Configures the Razor Pages options.
        /// </summary>
        /// <param name="options">Razor Pages options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The Razor Pages options</returns>
        public virtual RazorPagesOptions Options(RazorPagesOptions options, IConfiguration configuration, IHostEnvironment environment) => options;
    }
}
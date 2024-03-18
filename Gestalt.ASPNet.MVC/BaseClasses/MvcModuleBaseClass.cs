using Gestalt.ASPNet.BaseClasses;
using Gestalt.ASPNet.MVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.MVC.BaseClasses
{
    /// <summary>
    /// MVC Module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="AspNetModuleBaseClass{TModule}"/>
    /// <seealso cref="IMvcModule"/>
    public abstract class MvcModuleBaseClass<TModule> : AspNetModuleBaseClass<TModule>, IMvcModule
        where TModule : MvcModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvcModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="contentPath">The content path.</param>
        /// <param name="tags">The tags.</param>
        protected MvcModuleBaseClass(string? name, string? category, string? contentPath, params string[] tags)
            : base(name, category, contentPath, tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected MvcModuleBaseClass()
        {
        }

        /// <summary>
        /// Configures the MVC framework.
        /// </summary>
        /// <param name="mVCBuilder">MVC builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The MVC builder</returns>
        public virtual IMvcBuilder? ConfigureMVC(IMvcBuilder? mVCBuilder, IConfiguration configuration, IHostEnvironment environment) => mVCBuilder;

        /// <summary>
        /// Configures the MVC options.
        /// </summary>
        /// <param name="options">MVC options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The MVC options</returns>
        public virtual MvcOptions Options(MvcOptions options, IConfiguration configuration, IHostEnvironment environment) => options;
    }
}
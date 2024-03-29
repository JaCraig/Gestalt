﻿using Gestalt.ASPNet.BaseClasses;
using Gestalt.ASPNet.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestalt.ASPNet.Controllers.BaseClasses
{
    /// <summary>
    /// MVC controller only module base class
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="AspNetModuleBaseClass{TModule}"/>
    /// <seealso cref="IControllerModule"/>
    public abstract class ControllerModuleBaseClass<TModule> : AspNetModuleBaseClass<TModule>, IControllerModule
        where TModule : ControllerModuleBaseClass<TModule>, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerModuleBaseClass{TModule}"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="contentPath">The content path.</param>
        /// <param name="tags">The tags.</param>
        protected ControllerModuleBaseClass(string? name, string? category, string? contentPath, params string?[]? tags)
            : base(name, category, contentPath, tags)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerModuleBaseClass{TModule}"/> class.
        /// </summary>
        protected ControllerModuleBaseClass()
        {
        }

        /// <summary>
        /// Configures the MVC framework.
        /// </summary>
        /// <param name="mVCBuilder">MVC builder.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The MVC builder</returns>
        public virtual IMvcBuilder? ConfigureMVC(IMvcBuilder? mVCBuilder, IConfiguration? configuration, IHostEnvironment? environment) => mVCBuilder;

        /// <summary>
        /// Configures the MVC options.
        /// </summary>
        /// <param name="options">MVC options.</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">Host environment.</param>
        /// <returns>The MVC options</returns>
        public virtual MvcOptions? Options(MvcOptions? options, IConfiguration? configuration, IHostEnvironment? environment) => options;
    }
}
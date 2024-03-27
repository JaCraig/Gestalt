using Microsoft.AspNetCore.Mvc;

namespace Gestalt.Example.SelfContainedModule.Controllers
{
    /// <summary>
    /// Our health controller. This will return a simple view with the health status of the application that gets pulled from the health checks.
    /// </summary>
    public class HealthController : Controller
    {
        /// <summary>
        /// The index action for the health controller.
        /// </summary>
        /// <returns>The view.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
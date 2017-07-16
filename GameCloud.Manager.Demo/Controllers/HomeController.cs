using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameCloud.Manager.Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        public HomeController(ILoggerFactory logger_factory)
        {
            logger = logger_factory.CreateLogger("Default");
        }

        public IActionResult Index()
        {
            logger.LogInformation("HomeController.Index() ~~~~~~~~");

            return View();
        }

        public IActionResult Table()
        {
            return View();
        }

        public IActionResult Form()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

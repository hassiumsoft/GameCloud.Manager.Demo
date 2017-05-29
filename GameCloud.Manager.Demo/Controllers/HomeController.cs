using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameCloud.Manager.Demo.Controllers
{
    public class HomeController : Controller
    {
        //---------------------------------------------------------------------
        private readonly ILogger logger;

        //---------------------------------------------------------------------
        public HomeController(ILoggerFactory logger_factory)
        {
            logger = logger_factory.CreateLogger("Default");
        }

        //---------------------------------------------------------------------
        public IActionResult Index()
        {
            logger.LogInformation("HomeController.Index() ~~~~~~~~");

            return View();
        }

        //---------------------------------------------------------------------
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        //---------------------------------------------------------------------
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //---------------------------------------------------------------------
        public IActionResult Error()
        {
            return View();
        }
    }
}

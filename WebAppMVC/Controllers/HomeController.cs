using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.Models;
using WebAppMVC.Services;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebCheckClient _webCheckClient = WebCheckClient.GetInstance();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Terminal()
        {
            bool terminalStatus = _webCheckClient.CheckTerminalStatus();
            ViewBag.TerminalStatus = terminalStatus;
            return View();
        }

        public IActionResult RegisterDevice()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Signatures()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
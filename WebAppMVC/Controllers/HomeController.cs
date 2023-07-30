using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using WebAppMVC.Filters;
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

        [JwtAuthorize]
        public IActionResult Index()
        {
            string userEmail = Request.Cookies["UserEmail"];
            ViewBag.UserEmail = userEmail;

            return View();
        }

        [JwtAuthorize]
        public IActionResult Terminal()
        {
            bool terminalStatus = _webCheckClient.CheckTerminalStatus();
            ViewBag.TerminalStatus = terminalStatus;

            return View();
        }

        [JwtAuthorize]
        public IActionResult About()
        {
            return View();
        }

        [JwtAuthorize]
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
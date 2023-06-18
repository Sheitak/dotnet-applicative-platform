using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace WebAppMVC.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DeviceRepository _deviceRepository;

        public DeviceController(ILogger<HomeController> logger, DeviceRepository deviceRepository)
        {
            _logger = logger;
            _deviceRepository = deviceRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var device = await _deviceRepository.GetDevice(id);

            if (device != null)
            {
                ViewData["Device"] = device;
                return View(device);
            }
            else
            {
                ViewBag.ErrorMessage = "Appareil introuvable";
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(Device device, bool activate)
        {
            try
            {
                if (activate)
                {
                    device.IsActive = true;
                } else
                {
                    device.IsActive = false;
                }

                await _deviceRepository.PutDevice(device);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erreur de modification : {ex.Message}";
                return View("Error");
            }

            return RedirectToAction("Details", new { id = device.DeviceID });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebAppMVC.Filters;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace WebAppMVC.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DeviceRepository _deviceRepository;
        private readonly StudentRepository _studentRepository;

        public DeviceController(ILogger<HomeController> logger, DeviceRepository deviceRepository, StudentRepository studentRepository)
        {
            _logger = logger;
            _deviceRepository = deviceRepository;
            _studentRepository = studentRepository;
        }

        [JwtAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [JwtAuthorize]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = Request.Cookies["JwtToken"];

                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.ErrorMessage = "Token Bearer introuvable";
                    return View("Error");
                }

                var device = await _deviceRepository.GetDevice(id, token);

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
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la récupération des détails de l'appareil : {ex.Message}";
                return View("Error");
            }
        }

        public async Task<IActionResult> Create()
        {
            var students = new List<Student>();
            try
            {
                var token = Request.Cookies["JwtToken"];

                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.ErrorMessage = "Token Bearer introuvable";
                    return View("Error");
                }

                students = await _studentRepository.GetStudents(token);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la récupération de la liste des étudiants : {ex.Message}";
                return View("Error");
            }

            var studentListItems = students.Select(s => new SelectListItem
            {
                Text = s.Firstname + ' ' + s.Lastname,
                Value = s.StudentID.ToString()
            }).ToList();

            if (studentListItems != null)
            {
                ViewBag.StudentList = studentListItems;
            }
            else
            {
                ViewBag.StudentList = new List<SelectListItem>();
            }

            return View();
        }

        [HttpPost]
        [JwtAuthorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device)
        {
            try
            {
                var token = Request.Cookies["JwtToken"];

                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.ErrorMessage = "Token Bearer introuvable";
                    return View("Error");
                }

                await _deviceRepository.PostDevice(device, token);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la modification de l'appareil : {ex.Message}";
                return View("Error");
            }

            //return RedirectToAction("Details", new { id = device.DeviceID });
            return RedirectToAction("Index");
        }

        [JwtAuthorize]
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

                var token = Request.Cookies["JwtToken"];

                if (string.IsNullOrEmpty(token))
                {
                    ViewBag.ErrorMessage = "Token Bearer introuvable";
                    return View("Error");
                }

                await _deviceRepository.PutDevice(device, token);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la modification de l'appareil : {ex.Message}";
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

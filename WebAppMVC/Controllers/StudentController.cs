using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.Filters;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace WebAppMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentRepository _studentRepository;

        public StudentController(ILogger<HomeController> logger, StudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        [JwtAuthorize]
        public IActionResult Index()
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

                var student = await _studentRepository.GetStudent(id, token);

                if (student != null)
                {
                    ViewData["Student"] = student;
                    return View(student);
                }
                else
                {
                    ViewBag.ErrorMessage = "Etudiant introuvable";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur s'est produite lors de la récupération des détails de l'étudiant : {ex.Message}";
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
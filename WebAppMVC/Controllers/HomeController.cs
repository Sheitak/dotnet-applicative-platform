using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentRepository _studentRepository;

        public HomeController(ILogger<HomeController> logger, StudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Students()
        {
            var allStudents = await _studentRepository.GetAllStudents();

            if (allStudents != null)
            {
                ViewData["Students"] = allStudents;
            }

            return View(allStudents);
        }

        public async Task<IActionResult> Student(int id)
        {
            // TODO: passer le paramètre à GetStudent
            var student = await _studentRepository.GetStudent();

            if (student != null)
            {
                ViewData["Students"] = student;
            }

            return View(student);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
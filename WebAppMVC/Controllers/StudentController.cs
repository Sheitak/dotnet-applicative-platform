using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetStudent(id);

            if (student != null)
            {
                ViewData["Student"] = student;
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
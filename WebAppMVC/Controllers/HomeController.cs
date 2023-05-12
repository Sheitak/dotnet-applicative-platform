﻿using Microsoft.AspNetCore.Mvc;
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
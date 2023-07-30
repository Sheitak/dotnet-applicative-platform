using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.Repositories;

namespace WebAppMVC.Controllers
{
    public class SecurityController : Controller
    {
        private readonly SecurityRepository _securityRepository;

        public SecurityController(SecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var user = new User { Email = email, Password = password };

                var auth = await _securityRepository.Authentication(user);

                if (auth != null && auth.Token != null)
                {
                    ViewData["JwtToken"] = auth.Token;

                    Response.Cookies.Append("JwtToken", auth.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddMinutes(60)
                    });

                    if (!string.IsNullOrEmpty(auth.User?.Email))
                    {
                        Response.Cookies.Append("UserEmail", auth.User.Email, new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddMinutes(60)
                        });
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email ou mot de passe incorrect. Veuillez réessayer.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect. Veuillez réessayer : " + Environment.NewLine + ex.Message);
                return View();
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("JwtToken");
            Response.Cookies.Delete("UserEmail");
            return RedirectToAction("Login");
        }
    }
}

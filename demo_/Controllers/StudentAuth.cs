using System;
using Microsoft.AspNetCore.Mvc;
using demo_.Data;
using demo_.Models;
using demo_.Models.ViewModel;

namespace demo_.Controllers
{
    public class StudentAuth : Controller
    {
        public class AccountController : Controller
        {
            private readonly ApplicationDbContext _context;

            public AccountController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Login(LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = _context.Students.FirstOrDefault(u => u.email == model.Email);
                    if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                    {
                        HttpContext.Session.SetString("UserId", user.id.ToString());
                        HttpContext.Session.SetString("UserRole", user.Role);

                        if (user.Role == "Admin")
                            return RedirectToAction("AdminDashboard", "Admin");
                        else
                            return RedirectToAction("UserDashboard", "User");
                    }
                    ModelState.AddModelError("", "Invalid email or password.");
                }
                return View(model);
            }

            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
        }
    }
}


using demo_.Data;
using demo_.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using BCrypt.Net;


using demo_.Models;
using demo_.Models.user;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace demo_.Controllers

{
   
    public class StudentAuthController : Controller
    {
     
                private readonly ApplicationDbContext _context;

                public StudentAuthController(ApplicationDbContext context)
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
                if (user == null)
                    ModelState.AddModelError("", "Invalid email or password.");

                if (user != null && model.Password == user.Password)
                {
                    Guid Userid = new Guid();
                    Userid = user.id;
                    var claims = new List<Claim>
             {
         new Claim(ClaimTypes.Name, user.name),
         new Claim(ClaimTypes.Role, user.Role)
                };
                    var identity = new ClaimsIdentity(claims, "Login");
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(principal);


                    if (user.Role == "Admin")
                        return RedirectToAction("List", "Student");
                    else
                        return RedirectToAction("UserList", "User", new { id = Userid });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
                //if (!ModelState.IsValid)
                //        ModelState.AddModelError("", "Invalid email or password.");
                
            }
            return RedirectToAction("auth", "StudentAuth",model);
        }
                public IActionResult auth(LoginViewModel model)
                {  
                    return View(model);
                }

                public IActionResult Logout()
                {
                    
                    HttpContext.SignOutAsync();
                    return RedirectToAction("Login");
                }
            }
        }
    





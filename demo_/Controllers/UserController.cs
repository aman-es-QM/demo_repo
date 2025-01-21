using Microsoft.AspNetCore.Mvc;
using demo_.Models.user;
using Microsoft.EntityFrameworkCore;
using demo_.Data;
using demo_.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace demo_.Controllers
{
    
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbConetxt;
        public UserController(ApplicationDbContext dbConetxt)
        {
            this.dbConetxt = dbConetxt;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> UserList(Guid Id)
        {
            var student = await dbConetxt.Students.FindAsync(Id);

            return View(student);
        }
         
       
        
    }   
}

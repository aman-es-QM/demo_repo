using System.Security.Claims;
using demo_.Data;
using demo_.Models;
using demo_.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace demo_.Controllers
{
    
    public class StudentController : Controller

    {
        
        private readonly ApplicationDbContext dbConetxt;

        public StudentController(ApplicationDbContext dbConetxt)
        {
            this.dbConetxt = dbConetxt;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var student = new Student
            {
                name = viewModel.name,
                email = viewModel.email,
                phone = viewModel.phone,
                Password= viewModel.Password,
                
                Role=viewModel.Role,
                CreatedAt=DateTime.UtcNow,
                UpdatedAt=DateTime.UtcNow


            };
            await dbConetxt.Students.AddAsync(student);
            await dbConetxt.SaveChangesAsync();
            return RedirectToAction("List","Student");
            
            
            
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> List(String sort, String Search) 
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewData["EmailSortParm"] = sort == "Email" ? "email_desc" : "Email";
            var student = await dbConetxt.Students.ToListAsync();
            if (!String.IsNullOrEmpty(Search))
            {
                student = student.Where(X => X.name.StartsWith(Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (sort)
            {
                case "name_desc":
                    student = student.OrderByDescending(s => s.name).ToList();
                    break;
                case "Email":
                    student = student.OrderBy(s => s.email).ToList();
                    break;
                case "email_desc":
                    student = student.OrderByDescending(s => s.email).ToList();
                    break;
                case "":
                    student = student.OrderBy(s => s.name).ToList();
                    break;
                default:
                    
                    break;
            }
            //const int pageSize = 10;
            //if(pg<1)
            //    pg = 1;
            //int recscount=student.Count();
            //var pager = new Pager(recscount, pg, pageSize);
            //int recskip = (pg-1)*pageSize;
            //var data=student.Skip(recskip).Take(pager.PageSize).ToList();
            //this.ViewBag.Pager = pager;
            return View(student);
            //return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {   
            var Student = await dbConetxt.Students.FindAsync(id);
            return View(Student); 
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var userrole = User.FindFirstValue(ClaimTypes.Role);
            var student = await dbConetxt.Students.FindAsync(viewModel.id);
            if (student is not null)
            { 
                student.name = viewModel.name;
                student.email = viewModel.email;    
                student.phone = viewModel.phone;
                student.Password = viewModel.Password;
                student.Role = viewModel.Role;
                student.UpdatedAt = DateTime.UtcNow;
                await dbConetxt.SaveChangesAsync();
            }
            if (userrole == "Admin")
                return RedirectToAction("List", "Student");
            else if(userrole=="User")
                return RedirectToAction("UserList", "User",new { id=student.id});
            return View(student);
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewMoedl)
        {
            var student = await dbConetxt.Students.AsNoTracking().FirstOrDefaultAsync(x => x.id == viewMoedl.id);
            if (student is not null)
            {
                dbConetxt.Students.Remove(viewMoedl);
                await dbConetxt.SaveChangesAsync();
            }
            return RedirectToAction("Login", "StudentAuth");
        }
        //[HttpGet]
        //public async Task<IActionResult> ListSearch(String searchstring)
        //{
        //    var students = await dbConetxt.Students.ToListAsync();
        //    if (!String.IsNullOrEmpty(searchstring))
        //    {
        //        students = students.Where(X => X.name.Contains(searchstring)).ToList();
        //    }
        //    if (String.IsNullOrEmpty(searchstring))
        //    {
        //        return RedirectToAction("List", "Student");
        //    }
        //    return View(students);
        //}


    }
}

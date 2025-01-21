using demo_.Models.Entity;
using demo_.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace demo_.Data
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options):base(options) 
        {
            
        }
        public DbSet<Student> Students  { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}

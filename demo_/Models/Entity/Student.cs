using System.ComponentModel.DataAnnotations;

namespace demo_.Models.Entity
{
    public class Student 
    {
        

        public Guid id { get; set; }
        [Required(ErrorMessage = "field required")]
        public string name { get; set; }
        [Required(ErrorMessage ="field required")]
        public string email { get; set; }
        [PhoneNumberValidation]
        public string phone { get; set; }
        public string Password { get; set; } // For authentication
        public string Role { get; set; } // Role (e.g., "Admin", "User")
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Optional: track creation time
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Optional: track updates
    }



}



using demo_.Models.Entity;
using demo_.Models.ViewModel;

namespace demo_.Models
{
    public class AddStudentViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        [PhoneNumberValidation]
        public string phone { get; set; }
        public string Password { get; set; } // For authentication
        public string Role { get; set; } // Role (e.g., "Admin", "User")
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Optional: track creation time
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Optional: track updates
    }

}


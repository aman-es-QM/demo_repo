using demo_.Models.Entity;

namespace demo_.Models.user
{
    public class user
    {
        public Guid id { get; set; }
        
        public string name { get; set; }
      
        public string email { get; set; }
      
        public string phone { get; set; }
        public string Password { get; set; } // For authentication
        public string Role { get; set; } // Role (e.g., "Admin", "User")
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Optional: track creation time
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Optional: track updates
    }
}

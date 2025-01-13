using Microsoft.AspNetCore.Identity;

namespace cmcs_api.Models

{
    public class Users : IdentityUser
    {
        public DateOnly Date { get; set; }

        public int UserID { get; set; }
        public int Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}

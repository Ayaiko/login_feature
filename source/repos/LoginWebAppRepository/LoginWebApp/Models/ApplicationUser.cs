using Microsoft.AspNetCore.Identity;

namespace LoginWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
    }
}

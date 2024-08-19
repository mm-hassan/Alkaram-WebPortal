using Microsoft.AspNetCore.Identity;

namespace WebPortal.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Fullname { get; set; }

        public string Division { get; set; }
    }
}

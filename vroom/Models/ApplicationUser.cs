using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace vroom.Models
{
    public class ApplicationUser : IdentityUser
    {
        //[NotMapped]
        public bool IsAdmin { get; set; }
    }
}

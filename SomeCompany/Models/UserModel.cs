using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SomeCompany.Enums;
using System.ComponentModel;

namespace SomeCompany.Models
{
    public class UserModel : IdentityUser
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Last Login Time")]
        public DateTime LastLoginTime { get; set; }
        [DisplayName("Registration Time")]
        public DateTime RegistrationTime { get; set; }
        [DisplayName("IsBlocked")]
        public bool IsBlocked { get; set; } = false;
    }
}

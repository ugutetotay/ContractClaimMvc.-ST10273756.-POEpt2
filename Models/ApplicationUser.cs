using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractClaimMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = "Student"; // Student or Lecturer
    }
}
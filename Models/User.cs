using System.ComponentModel.DataAnnotations;

namespace ContractClaimMvc.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Lecturer";
        public decimal HourlyRate { get; set; }
        public string Department { get; set; } = "Academic";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
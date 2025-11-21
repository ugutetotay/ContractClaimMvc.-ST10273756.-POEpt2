using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractClaimMvc.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required]
        public int LecturerId { get; set; } // Links to User who submitted

        [Required]
        [Display(Name = "Lecturer Name")]
        public string LecturerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Hours Worked")]
        [Range(1, 200, ErrorMessage = "Hours must be between 1 and 200")]
        public decimal HoursWorked { get; set; }

        [Required]
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; } // From User table (set by HR)

        [Display(Name = "Total Amount")]
        public decimal TotalAmount => HoursWorked * HourlyRate;

        [Display(Name = "Additional Notes")]
        public string Notes { get; set; } = string.Empty;

        [Display(Name = "Document Path")]
        public string? DocumentPath { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        [Display(Name = "Submitted Date")]
        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        [Display(Name = "Processed Date")]
        public DateTime? ProcessedDate { get; set; }

        public int? ProcessedById { get; set; } // Coordinator/Manager who approved/rejected

        [Display(Name = "Processed By")]
        public string? ProcessedByName { get; set; }

        [NotMapped]
        [Display(Name = "Upload Document")]
        public IFormFile? DocumentFile { get; set; }
    }
}
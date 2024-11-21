using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10150702_PROG6212_POE.Models
{
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimID { get; set; }

        [Required]
        public int LecturerID { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total hours worked must be a positive value.")]
        public decimal TotalHoursWorked { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount due must be a positive value.")]
        public decimal AmountDue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be a positive value.")]
        public decimal HourlyRate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = "Pending";

        [Required]
        [StringLength(20, ErrorMessage = "Module code cannot exceed 20 characters.")]
        public string ModuleCode { get; set; } = string.Empty;

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Document path cannot exceed 255 characters.")]
        public string? DocumentPath { get; set; }
    }
}

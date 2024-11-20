using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

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
        public decimal TotalHoursWorked { get; set; }  

        [Required]
        [Column(TypeName = "decimal(18,2)")]  
        public decimal AmountDue { get; set; }

        [Column(TypeName = "decimal(18,2)")]  
        public decimal HourlyRate { get; set; }

        public string Status { get; set; } = "Pending";

        public string ModCode { get; set; }

        public string desc { get; set; }

        public string? DocumentPath { get; set; }
    }
}
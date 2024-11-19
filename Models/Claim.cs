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
        public int TotalHoursWorked { get; set; }

        [Required]
        public decimal AmountDue {  get; set; }
        public string Status { get; set; } = "Pending"; //set this to default
        public string ModCode { get; set; }

        public string desc { get; set; }

        public string? DocumentPath { get; set; }

    }
}




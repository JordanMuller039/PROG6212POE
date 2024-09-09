using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
namespace ST10150702_PROG6212_POE.Models
{
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; }
        public int LecturerID { get; set; }
        public int TotalHoursWorked { get; set; }
        public decimal AmountDue {  get; set; }
        public string Status {  get; set; }

    }
}

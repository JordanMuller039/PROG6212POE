using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
namespace ST10150702_PROG6212_POE.Models
{
    public class Lecturer
    {
        [Key]
        public int LectID { get; set; } 
        public string? LecName { get; set; }
        public string? LecSurname { get; set; }
        public decimal HourlyRate { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int Claims {  get; set; }

    }
}

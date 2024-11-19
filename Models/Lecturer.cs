using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ST10150702_PROG6212_POE.Models
{
    public class Lecturer : IdentityUser
    {
        [Key]
        public int LectID { get; set; }
        public string LecName { get; set; }
        public string LecSurname { get; set; }
        public decimal HourlyRate { get; set; }
        public int Claims { get; set; }
        public bool IsManager { get; set; }
    }
}
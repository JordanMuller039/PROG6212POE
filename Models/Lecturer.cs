using System.ComponentModel.DataAnnotations;

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

        // Role flags
        public bool IsLecturer { get; set; } = false;
        public bool IsManager { get; set; } = false;
        public bool IsHR { get; set; } = false;
    }
}
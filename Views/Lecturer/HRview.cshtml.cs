using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ST10150702_PROG6212_POE.Views.Lecturer
{
    public class HRviewModel
    {
        public List<Claim> Claims { get; set; }

        public HRviewModel()
        {
            // Populate the Claims list with data from your database
            Claims = new List<Claim>
        {
            new Claim { ClaimID = 1, Claimant = "John Doe", Status = "Approved", Date = DateTime.Now.AddDays(-1) },
            new Claim { ClaimID = 2, Claimant = "Jane Smith", Status = "Pending", Date = DateTime.Now.AddDays(-3) },
            // Add more sample claims as needed
        };
        }
    }

    public class Claim
    {
        public int ClaimID { get; set; }
        public string Claimant { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }

}

using Microsoft.AspNetCore.Mvc;
using ST10150702_PROG6212_POE.Models;
using ST10150702_PROG6212_POE.Data;
using Microsoft.EntityFrameworkCore;

namespace ST10150702_PROG6212_POE.Controllers
{
    public class ClaimController : Controller
    {
        private readonly AppDBContext _context;

        public ClaimController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Verify()
        {
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Set default status if not provided
                if (string.IsNullOrWhiteSpace(claim.Status))
                {
                    claim.Status = "Pending"; // Default status
                }

                await _context.Claims.AddAsync(claim);
                await _context.SaveChangesAsync(); // This will save the new claim and generate ClaimID automatically

                return Json(new { success = true, claimId = claim.ClaimID }); // Optionally return the new ClaimID
            }

            // Handle validation errors
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage)).ToList();
            return Json(new { success = false, errors });
        }




    }
}

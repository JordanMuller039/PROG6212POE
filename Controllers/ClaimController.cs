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


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var claim = _context.Claims.Find(id); // Find the claim by ID
            if (claim == null)
            {
                // Return a JSON response with success: false if claim is not found
                return Json(new { success = false, message = "Claim not found" });
            }

            _context.Claims.Remove(claim); // Remove the claim
            _context.SaveChanges(); // Save changes to the database

            // Return a JSON response indicating success
            return Json(new { success = true, message = "Claim deleted successfully" });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Claim claim)
        {
            // Explicitly validate the model
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new
                {
                    success = false,
                    errors = errors
                });
            }

            try
            {
                // Set default status if not provided
                if (string.IsNullOrWhiteSpace(claim.Status))
                {
                    claim.Status = "Pending"; // Default status
                }

                // Add the claim to the context
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    claimId = claim.ClaimID
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new
                {
                    success = false,
                    errors = new[] { "An error occurred while creating the claim: " + ex.Message }
                });
            }
        }



    }
}

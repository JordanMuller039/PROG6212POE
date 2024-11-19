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
        public async Task<IActionResult> Create(Claim claim, List<IFormFile> supportingImages)
        {
            if (ModelState.IsValid)
            {
                // Set default status if not provided
                if (string.IsNullOrWhiteSpace(claim.Status))
                {
                    claim.Status = "Pending"; // Default status
                }

                // Initialize DocumentPath to null or empty
                claim.DocumentPath = null;

                // Handle document uploads if there are any
                if (supportingImages != null && supportingImages.Count > 0)
                {
                    var documentPaths = new List<string>();

                    foreach (var file in supportingImages)
                    {
                        if (file.Length > 0)
                        {
                            var filePath = Path.Combine("uploads", file.FileName); // Adjust path as needed
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            documentPaths.Add(filePath); // Save the path
                        }
                    }

                    // Join document paths into a single string if there are any documents
                    if (documentPaths.Count > 0)
                    {
                        claim.DocumentPath = string.Join(",", documentPaths);
                    }
                }

                await _context.Claims.AddAsync(claim);
                await _context.SaveChangesAsync();

                return Json(new { success = true, claimId = claim.ClaimID });
            }

            // Log ModelState errors for debugging
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Optionally log errors to console (or your preferred logging mechanism)
            foreach (var error in errors)
            {
                Console.WriteLine(error); // Use your logging method here
            }

            // Return errors in the response
            return Json(new { success = false, errors });
        }



    }
}

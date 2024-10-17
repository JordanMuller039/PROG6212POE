using Microsoft.AspNetCore.Mvc;
using ST10150702_PROG6212_POE.Models;
using ST10150702_PROG6212_POE.Data;
using Microsoft.EntityFrameworkCore;
namespace ST10150702_PROG6212_POE.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AppDBContext _context;

        public LecturerController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Lecturers.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateLogin()
            { return View(); }

        public IActionResult Verify()
        {
            var claims = _context.Claims.ToList(); // Assuming you're using Entity Framework
            if (claims == null || !claims.Any())
            {
                claims = new List<Claim>(); // Return an empty list instead of null
            }
            return View(claims);
        }


        [HttpPost]
        public IActionResult CreateClaim()
        {
            var claims = _context.Claims.ToList(); // Fetch claims
            if (claims == null || !claims.Any())
            {
                claims = new List<Claim>(); // Return an empty list if no claims
            }

            return View(claims); // Pass claims to the view
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var claim = _context.Claims.Find(id); // Find the claim by ID
            if (claim == null)
            {
                return NotFound(); // Return a 404 if the claim doesn't exist
            }

            _context.Claims.Remove(claim); // Remove the claim
            _context.SaveChanges(); // Save changes to the database

            return Json(new { success = true }); // Return a success response
        }






    }
}

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


        [HttpPost]
        public IActionResult CreateClaim(string username, string password)
        {
            // Validate the username and password
            // If valid, return the CreateClaim view
            return View("CreateClaim");
        }

        public IActionResult CreateClaim()
        {
            var claims = _context.Claims.ToList(); // or however you're getting your claims
            return View(claims); // Pass the list of claims to the view
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
        

    }
}

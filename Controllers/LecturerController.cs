using Microsoft.AspNetCore.Mvc;
using ST10150702_PROG6212_POE.Models;
using ST10150702_PROG6212_POE.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ST10150702_PROG6212_POE.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LecturerController(AppDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
        {
            return View();
        }

        public IActionResult Verify()
        {
            var claims = _context.Claims.ToList();
            if (claims == null || !claims.Any())
            {
                claims = new List<Claim>();
            }
            return View(claims);
        }

        public IActionResult HRview()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [HttpPost]
        public IActionResult CreateClaim()
        {
            var claims = _context.Claims.ToList();
            if (claims == null || !claims.Any())
            {
                claims = new List<Claim>();
            }
            return View(claims);
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
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                return NotFound();
            }
            _context.Claims.Remove(claim);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                _context.SaveChanges();
                TempData["Message"] = "Claim approved successfully!";
            }
            return RedirectToAction("Verify");
        }

        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _context.SaveChanges();
                TempData["Message"] = "Claim rejected successfully!";
            }
            return RedirectToAction("Verify");
        }

    }
}
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

        // GET: /Lecturer
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Lecturer/CreateLogin
        public IActionResult CreateLogin()
        {
            return View();
        }

        // POST: /Lecturer/CreateAccount
        [HttpPost]
        public async Task<IActionResult> CreateAccount(Lecturer lecturer, string role)
        {
            if (ModelState.IsValid)
            {
                // Set the appropriate role flag based on selection
                switch (role.ToLower())
                {
                    case "lecturer":
                        lecturer.IsLecturer = true;
                        lecturer.IsManager = false;
                        lecturer.IsHR = false;
                        break;
                    case "manager":
                        lecturer.IsLecturer = false;
                        lecturer.IsManager = true;
                        lecturer.IsHR = false;
                        break;
                    case "hr":
                        lecturer.IsLecturer = false;
                        lecturer.IsManager = false;
                        lecturer.IsHR = true;
                        break;
                    default:
                        TempData["Error"] = "Please select a valid role";
                        return View("CreateLogin", lecturer);
                }

                // Check if username already exists
                if (_context.Lecturers.Any(l => l.UserName == lecturer.UserName))
                {
                    TempData["Error"] = "Username already exists";
                    return View("CreateLogin", lecturer);
                }

                try
                {
                    _context.Add(lecturer);
                    await _context.SaveChangesAsync();

                    // Store user ID in TempData
                    TempData["CurrentUserId"] = lecturer.LectID;

                    // Redirect based on role
                    if (lecturer.IsLecturer)
                        return RedirectToAction("CreateClaim");
                    else if (lecturer.IsManager)
                        return RedirectToAction("Verify");
                    else if (lecturer.IsHR)
                        return RedirectToAction("HRview");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the account";
                    return View("CreateLogin", lecturer);
                }
            }

            return View("CreateLogin", lecturer);
        }

        // POST: /Lecturer/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Lecturers
                .FirstOrDefault(l => l.UserName == username && l.Password == password);

            if (user == null)
            {
                TempData["Error"] = "Invalid username or password";
                return RedirectToAction("Index");
            }

            TempData["CurrentUserId"] = user.LectID;

            if (user.IsLecturer)
                return RedirectToAction("CreateClaim");
            else if (user.IsManager)
                return RedirectToAction("Verify");
            else if (user.IsHR)
                return RedirectToAction("HRview");

            TempData["Error"] = "User role not properly configured";
            return RedirectToAction("Index");
        }

        // GET: /Lecturer/Verify
        public IActionResult Verify()
        {
            var claims = _context.Claims.ToList();
            if (claims == null || !claims.Any())
            {
                claims = new List<Claim>();
            }
            return View(claims);
        }

        // GET: /Lecturer/HRview
        public IActionResult HRview()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        // GET: /Lecturer/CreateClaim
        public IActionResult CreateClaim()
        {
            var claims = _context.Claims.ToList();
            if (claims == null || !claims.Any())
            {
                claims = new List<Claim>();
            }
            return View(claims);
        }

        // POST: /Lecturer/Delete
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

        // POST: /Lecturer/Approve
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

        // POST: /Lecturer/Reject
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
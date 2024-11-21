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
            if (TempData["Error"] != null)
            {
                ViewBag.ErrorMessage = TempData["Error"];
            }
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
                try
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

            // If we get here, something was invalid
            TempData["Error"] = "Please fill in all required fields correctly";
            return View("CreateLogin", lecturer);
        }


        // GET: /Lecturer/ManageLecturers
        public IActionResult ManageLecturers()
        {
            var lecturers = _context.Lecturers.ToList();  
            return View(lecturers); 
        }

        [HttpPost]
        public IActionResult UpdateLecturer([FromBody] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                var existingLecturer = _context.Lecturers.FirstOrDefault(l => l.LectID == lecturer.LectID);
                if (existingLecturer == null)
                {
                    return Json(new { success = false, message = "Lecturer not found." });
                }

                // Update the lecturer's details
                existingLecturer.UserName = lecturer.UserName;
                existingLecturer.LecName = lecturer.LecName;
                existingLecturer.Password = lecturer.Password;
                existingLecturer.LecSurname = lecturer.LecSurname;
                existingLecturer.HourlyRate = lecturer.HourlyRate;

                _context.SaveChanges();

                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invalid data." });
        }



        // POST: /Lecturer/Login
        [HttpPost]
        public IActionResult Login(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.ErrorMessage = "Username cannot be empty.";
                return View("Index", new List<Lecturer>()); // Pass an empty model
            }

            // Check if the username exists in the database
            var user = _context.Lecturers.SingleOrDefault(u => u.UserName == username);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username. Please try again.";
                return View("Index", new List<Lecturer>()); // Pass an empty model
            }

            // Redirect to appropriate view based on user role
            if (user.IsLecturer)
                return RedirectToAction("CreateClaim");
            else if (user.IsManager)
                return RedirectToAction("Verify");
            else if (user.IsHR)
                return RedirectToAction("HRView");

            // Default fallback
            ViewBag.ErrorMessage = "User role not configured correctly.";
            return View("Index", new List<Lecturer>()); // Pass an empty model
        }


        // GET: /Lecturer/Verify
        public IActionResult Verify()
        {
            try
            {
                var claims = _context.Claims.ToList() ?? new List<Claim>();
                return View(claims);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading verification page";
                return RedirectToAction("Index");
            }
        }

        // GET: /Lecturer/HRview
        public IActionResult HRview()
        {
            try
            {
                var claims = _context.Claims.ToList() ?? new List<Claim>();
                return View(claims);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading HR view";
                return RedirectToAction("Index");
            }
        }

        // GET: /Lecturer/CreateClaim
        public IActionResult CreateClaim()
        {
            try
            {
                var claims = _context.Claims.ToList() ?? new List<Claim>();
                return View(claims);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error loading claims page";
                return RedirectToAction("Index");
            }
        }

        // POST: /Lecturer/Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var claim = _context.Claims.Find(id);
                if (claim == null)
                {
                    return Json(new { success = false, message = "Claim not found" });
                }

                _context.Claims.Remove(claim);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting claim" });
            }
        }

        // POST: /Lecturer/Approve
        public IActionResult Approve(int id)
        {
            try
            {
                var claim = _context.Claims.Find(id);
                if (claim == null)
                {
                    TempData["Error"] = "Claim not found";
                    return RedirectToAction("Verify");
                }

                claim.Status = "Approved";
                _context.SaveChanges();
                TempData["Message"] = "Claim approved successfully!";
                return RedirectToAction("Verify");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error approving claim";
                return RedirectToAction("Verify");
            }
        }

        // POST: /Lecturer/Reject
        public IActionResult Reject(int id)
        {
            try
            {
                var claim = _context.Claims.Find(id);
                if (claim == null)
                {
                    TempData["Error"] = "Claim not found";
                    return RedirectToAction("Verify");
                }

                claim.Status = "Rejected";
                _context.SaveChanges();
                TempData["Message"] = "Claim rejected successfully!";
                return RedirectToAction("Verify");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error rejecting claim";
                return RedirectToAction("Verify");
            }
        }
    }
}
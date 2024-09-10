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
    }
}

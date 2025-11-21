using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public LecturerController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Lecturer Dashboard - Only their claims
        public async Task<IActionResult> Index()
        {
            // Session authorization check (REQUIRED)
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Lecturer"))
                return Empty;

            var userId = SessionHelper.GetUserId(HttpContext.Session).Value;
            var claims = await _context.Claims
                .Where(c => c.LecturerId == userId)
                .OrderByDescending(c => c.SubmittedDate)
                .ToListAsync();

            ViewBag.UserName = SessionHelper.GetUserName(HttpContext.Session);
            return View(claims);
        }

        // GET: Submit claim form
        public IActionResult Submit()
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Lecturer"))
                return Empty;

            return View();
        }

        // POST: Submit claim with automation
        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Lecturer"))
                return Empty;

            if (ModelState.IsValid)
            {
                var userId = SessionHelper.GetUserId(HttpContext.Session).Value;
                var lecturer = await _context.Users.FindAsync(userId);

                // Automated data population (REQUIRED)
                claim.LecturerId = userId;
                claim.LecturerName = lecturer.FullName;
                claim.HourlyRate = lecturer.HourlyRate; // From HR setting (not editable)
                claim.Status = "Pending";
                claim.SubmittedDate = DateTime.Now;

                // File upload handling
                if (claim.DocumentFile != null && claim.DocumentFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "documents");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + claim.DocumentFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await claim.DocumentFile.CopyToAsync(stream);
                    }

                    claim.DocumentPath = uniqueFileName;
                }

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Claim submitted successfully! It is now pending approval.";
                return RedirectToAction("Index");
            }

            return View(claim);
        }

        // Download file
        public IActionResult DownloadFile(string fileName)
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Lecturer"))
                return Empty;

            if (string.IsNullOrEmpty(fileName))
                return NotFound();

            var filePath = Path.Combine(_environment.WebRootPath, "documents", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}
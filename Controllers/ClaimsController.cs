using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ClaimsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Display all claims
        public async Task<IActionResult> Index()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }

        // GET: Submit claim form
        public IActionResult Submit()
        {
            return View();
        }

        // POST: Submit new claim
        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
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

                claim.Status = "Pending";
                claim.SubmittedDate = DateTime.Now;

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction(nameof(Track));
            }
            return View(claim);
        }

        // GET: Approve claims (for coordinators)
        public async Task<IActionResult> Approve()
        {
            var pendingClaims = await _context.Claims
                .Where(c => c.Status == "Pending")
                .ToListAsync();
            return View(pendingClaims);
        }

        // POST: Approve a claim
        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                claim.ProcessedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim approved successfully!";
            }
            return RedirectToAction(nameof(Approve));
        }

        // POST: Reject a claim
        [HttpPost]
        public async Task<IActionResult> RejectClaim(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                claim.ProcessedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim rejected!";
            }
            return RedirectToAction(nameof(Approve));
        }

        // GET: Track claims
        public async Task<IActionResult> Track()
        {
            var claims = await _context.Claims.ToListAsync();
            return View(claims);
        }
    }
}
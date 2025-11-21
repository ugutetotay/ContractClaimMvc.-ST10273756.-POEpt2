using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly AppDbContext _context;

        public CoordinatorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Coordinator Dashboard - Pending claims
        public async Task<IActionResult> Index()
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Coordinator"))
                return Empty;

            var pendingClaims = await _context.Claims
                .Where(c => c.Status == "Pending")
                .OrderBy(c => c.SubmittedDate)
                .ToListAsync();

            ViewBag.UserName = SessionHelper.GetUserName(HttpContext.Session);
            return View(pendingClaims);
        }

        // POST: Approve claim
        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Coordinator"))
                return Empty;

            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                var userId = SessionHelper.GetUserId(HttpContext.Session).Value;
                var userName = SessionHelper.GetUserName(HttpContext.Session);

                claim.Status = "Approved";
                claim.ProcessedDate = DateTime.Now;
                claim.ProcessedById = userId;
                claim.ProcessedByName = userName;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim approved successfully!";
            }

            return RedirectToAction("Index");
        }

        // POST: Reject claim
        [HttpPost]
        public async Task<IActionResult> RejectClaim(int id)
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Coordinator"))
                return Empty;

            var claim = await _context.Claims.FindAsync(id);
            if (claim != null)
            {
                var userId = SessionHelper.GetUserId(HttpContext.Session).Value;
                var userName = SessionHelper.GetUserName(HttpContext.Session);

                claim.Status = "Rejected";
                claim.ProcessedDate = DateTime.Now;
                claim.ProcessedById = userId;
                claim.ProcessedByName = userName;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim has been rejected.";
            }

            return RedirectToAction("Index");
        }
    }
}
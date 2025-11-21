using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class ManagerController : Controller
    {
        private readonly AppDbContext _context;

        public ManagerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Manager Dashboard - All claims overview
        public async Task<IActionResult> Index()
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Manager"))
                return Empty;

            var allClaims = await _context.Claims
                .OrderByDescending(c => c.SubmittedDate)
                .ToListAsync();

            ViewBag.UserName = SessionHelper.GetUserName(HttpContext.Session);
            ViewBag.PendingCount = allClaims.Count(c => c.Status == "Pending");
            ViewBag.ApprovedCount = allClaims.Count(c => c.Status == "Approved");
            ViewBag.TotalAmount = allClaims.Where(c => c.Status == "Approved").Sum(c => c.TotalAmount);

            return View(allClaims);
        }

        // POST: Approve claim (Manager can also approve)
        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            if (!SessionHelper.RequireRole(HttpContext.Session, Response, "Manager"))
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
    }
}
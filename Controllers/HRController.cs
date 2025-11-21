using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class HRController : Controller
    {
        private readonly AppDbContext _context;
        public HRController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            if (!SessionHelper.HasRole(HttpContext.Session, "HR")) return RedirectToAction("Login", "Account");
            var users = await _context.Users.Where(u => u.Role != "HR").ToListAsync();
            return View(users);
        }

        public IActionResult CreateUser() => SessionHelper.HasRole(HttpContext.Session, "HR") ? View() : RedirectToAction("Login", "Account");

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!SessionHelper.HasRole(HttpContext.Session, "HR")) return RedirectToAction("Login", "Account");
            if (string.IsNullOrEmpty(user.EmployeeId))
                user.EmployeeId = GenerateEmployeeId(user.Role);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"{user.Role} account created!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUser(int id)
        {
            if (!SessionHelper.HasRole(HttpContext.Session, "HR")) return RedirectToAction("Login", "Account");
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            if (!SessionHelper.HasRole(HttpContext.Session, "HR")) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<IActionResult> Reports()
        {
            if (!SessionHelper.HasRole(HttpContext.Session, "HR")) return RedirectToAction("Login", "Account");
            var claims = await _context.Claims.Where(c => c.Status == "Approved").ToListAsync();
            return View(claims);
        }

        private string GenerateEmployeeId(string role) => $"{(role == "Lecturer" ? "LEC" : role == "Coordinator" ? "CO" : "MGR")}{new Random().Next(1000, 9999)}";
    }
}
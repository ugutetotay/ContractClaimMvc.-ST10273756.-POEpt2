using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            // Clear any existing session
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string employeeId, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Please enter both Employee ID and Password.");
                    return View();
                }

                // Debug: Check if we can access the database
                var userCount = await _context.Users.CountAsync();
                Console.WriteLine($"Total users in database: {userCount}");

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.EmployeeId == employeeId && u.Password == password && u.IsActive);

                if (user != null)
                {
                    // Store user info in session
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("EmployeeId", user.EmployeeId);
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetString("Role", user.Role);
                    HttpContext.Session.SetString("Email", user.Email);

                    TempData["SuccessMessage"] = $"Welcome back, {user.FullName}!";

                    // Redirect based on role
                    return RedirectToDashboard(user.Role);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Employee ID or Password.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Login error: {ex.Message}");
                ModelState.AddModelError("", "Database error. Please check if database is properly seeded.");
                return View();
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }

        private IActionResult RedirectToDashboard(string role)
        {
            return role switch
            {
                "HR" => RedirectToAction("Index", "HR"),
                "Lecturer" => RedirectToAction("Index", "Lecturer"),
                "Coordinator" => RedirectToAction("Index", "Coordinator"),
                "Manager" => RedirectToAction("Index", "Manager"),
                _ => RedirectToAction("Login")
            };
        }
    }
}
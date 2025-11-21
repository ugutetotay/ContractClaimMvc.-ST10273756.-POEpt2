using Microsoft.AspNetCore.Mvc;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Redirect to login if not authenticated
            if (!SessionHelper.IsLoggedIn(HttpContext.Session))
                return RedirectToAction("Login", "Account");

            // Redirect to appropriate dashboard based on role
            var role = SessionHelper.GetUserRole(HttpContext.Session);
            return role switch
            {
                "HR" => RedirectToAction("Index", "HR"),
                "Lecturer" => RedirectToAction("Index", "Lecturer"),
                "Coordinator" => RedirectToAction("Index", "Coordinator"),
                "Manager" => RedirectToAction("Index", "Manager"),
                _ => RedirectToAction("Login", "Account")
            };
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
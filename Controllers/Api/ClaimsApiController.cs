using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

namespace ContractClaimMvc.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClaimsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/claims/calculate - Automated calculation (REQUIRED)
        [HttpGet("calculate")]
        public ActionResult<decimal> CalculateClaim(int lecturerId, decimal hoursWorked)
        {
            var lecturer = _context.Users.Find(lecturerId);
            if (lecturer == null)
                return NotFound("Lecturer not found");

            if (hoursWorked < 1 || hoursWorked > 200)
                return BadRequest("Hours must be between 1 and 200");

            return hoursWorked * lecturer.HourlyRate;
        }

        // GET: api/claims/lecturer/{id} - Get lecturer's claims
        [HttpGet("lecturer/{id}")]
        public ActionResult<IEnumerable<Claim>> GetLecturerClaims(int id)
        {
            return _context.Claims.Where(c => c.LecturerId == id).ToList();
        }

        // GET: api/claims/pending - Get pending claims for coordinators
        [HttpGet("pending")]
        public ActionResult<IEnumerable<Claim>> GetPendingClaims()
        {
            return _context.Claims.Where(c => c.Status == "Pending").ToList();
        }

        // GET: api/claims/approved - Get approved claims for reports
        [HttpGet("approved")]
        public ActionResult<IEnumerable<Claim>> GetApprovedClaims()
        {
            return _context.Claims.Where(c => c.Status == "Approved").ToList();
        }

        // GET: api/claims/stats - Get system statistics
        [HttpGet("stats")]
        public ActionResult<object> GetSystemStats()
        {
            var totalClaims = _context.Claims.Count();
            var pendingClaims = _context.Claims.Count(c => c.Status == "Pending");
            var approvedAmount = _context.Claims.Where(c => c.Status == "Approved").Sum(c => c.TotalAmount);

            return new { totalClaims, pendingClaims, approvedAmount };
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace ContractClaimMvc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var staticDate = new DateTime(2025, 1, 1);

            builder.Entity<User>().HasData(
                new User { Id = 1, EmployeeId = "HR001", FullName = "HR Admin", Email = "hr@abc.com", Password = "123456", Role = "HR", HourlyRate = 0, Department = "Administration", CreatedDate = staticDate, IsActive = true },
                new User { Id = 2, EmployeeId = "LEC001", FullName = "John Lecturer", Email = "lecturer@abc.com", Password = "123456", Role = "Lecturer", HourlyRate = 250, Department = "Computer Science", CreatedDate = staticDate, IsActive = true },
                new User { Id = 3, EmployeeId = "CO001", FullName = "Sarah Coordinator", Email = "coordinator@abc.com", Password = "123456", Role = "Coordinator", HourlyRate = 0, Department = "Academic Affairs", CreatedDate = staticDate, IsActive = true },
                new User { Id = 4, EmployeeId = "MGR001", FullName = "Mike Manager", Email = "manager@abc.com", Password = "123456", Role = "Manager", HourlyRate = 0, Department = "Management", CreatedDate = staticDate, IsActive = true }
            );

            builder.Entity<Claim>().HasData(
                new Claim { ClaimId = 1, LecturerId = 2, LecturerName = "John Lecturer", HoursWorked = 40, HourlyRate = 250, Status = "Pending", SubmittedDate = staticDate.AddDays(-2), Notes = "Monthly teaching hours" },
                new Claim { ClaimId = 2, LecturerId = 2, LecturerName = "John Lecturer", HoursWorked = 35, HourlyRate = 250, Status = "Approved", SubmittedDate = staticDate.AddDays(-5), ProcessedDate = staticDate.AddDays(-1), ProcessedById = 3, ProcessedByName = "Sarah Coordinator", Notes = "Research supervision" }
            );
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ContractClaimMvc.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    // Check if users exist, if not seed them
    if (!db.Users.Any())
    {
        Console.WriteLine("Seeding database...");

        var users = new List<User>
        {
            new User { Id = 1, EmployeeId = "HR001", FullName = "HR Admin", Email = "hr@abc.com", Password = "123456", Role = "HR", HourlyRate = 0, Department = "Administration", CreatedDate = DateTime.Now, IsActive = true },
            new User { Id = 2, EmployeeId = "LEC001", FullName = "John Lecturer", Email = "lecturer@abc.com", Password = "123456", Role = "Lecturer", HourlyRate = 250, Department = "Computer Science", CreatedDate = DateTime.Now, IsActive = true },
            new User { Id = 3, EmployeeId = "CO001", FullName = "Sarah Coordinator", Email = "coordinator@abc.com", Password = "123456", Role = "Coordinator", HourlyRate = 0, Department = "Academic Affairs", CreatedDate = DateTime.Now, IsActive = true },
            new User { Id = 4, EmployeeId = "MGR001", FullName = "Mike Manager", Email = "manager@abc.com", Password = "123456", Role = "Manager", HourlyRate = 0, Department = "Management", CreatedDate = DateTime.Now, IsActive = true }
        };

        db.Users.AddRange(users);
        db.SaveChanges();
        Console.WriteLine("Database seeded successfully!");
    }
    else
    {
        Console.WriteLine($"Database already has {db.Users.Count()} users");
    }
}

app.Run();
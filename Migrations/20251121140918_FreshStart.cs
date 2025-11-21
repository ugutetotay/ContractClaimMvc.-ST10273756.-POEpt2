using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContractClaimMvc.Migrations
{
    /// <inheritdoc />
    public partial class FreshStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LecturerId = table.Column<int>(type: "INTEGER", nullable: false),
                    LecturerName = table.Column<string>(type: "TEXT", nullable: false),
                    HoursWorked = table.Column<decimal>(type: "TEXT", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    DocumentPath = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProcessedById = table.Column<int>(type: "INTEGER", nullable: true),
                    ProcessedByName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "ClaimId", "DocumentPath", "HourlyRate", "HoursWorked", "LecturerId", "LecturerName", "Notes", "ProcessedById", "ProcessedByName", "ProcessedDate", "Status", "SubmittedDate" },
                values: new object[,]
                {
                    { 1, null, 250m, 40m, 2, "Dr. John Smith", "Monthly teaching hours for August", null, null, null, "Pending", new DateTime(2025, 11, 19, 16, 9, 17, 334, DateTimeKind.Local).AddTicks(308) },
                    { 2, null, 250m, 35m, 2, "Dr. John Smith", "Research supervision hours", 3, "Prof. Sarah Wilson", new DateTime(2025, 11, 20, 16, 9, 17, 334, DateTimeKind.Local).AddTicks(1658), "Approved", new DateTime(2025, 11, 16, 16, 9, 17, 334, DateTimeKind.Local).AddTicks(1648) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Department", "Email", "EmployeeId", "FullName", "HourlyRate", "IsActive", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 21, 16, 9, 17, 329, DateTimeKind.Local).AddTicks(2288), "Administration", "hr@abc.com", "HR001", "HR Administrator", 0m, true, "123456", "HR" },
                    { 2, new DateTime(2025, 11, 21, 16, 9, 17, 332, DateTimeKind.Local).AddTicks(2790), "Computer Science", "lecturer@abc.com", "LEC001", "Dr. John Smith", 250m, true, "123456", "Lecturer" },
                    { 3, new DateTime(2025, 11, 21, 16, 9, 17, 332, DateTimeKind.Local).AddTicks(2859), "Academic Affairs", "coordinator@abc.com", "CO001", "Prof. Sarah Wilson", 0m, true, "123456", "Coordinator" },
                    { 4, new DateTime(2025, 11, 21, 16, 9, 17, 332, DateTimeKind.Local).AddTicks(2864), "Management", "manager@abc.com", "MGR001", "Dr. Michael Brown", 0m, true, "123456", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

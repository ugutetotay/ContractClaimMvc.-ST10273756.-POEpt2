using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractClaimMvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ClaimId",
                keyValue: 1,
                columns: new[] { "LecturerName", "Notes", "SubmittedDate" },
                values: new object[] { "John Lecturer", "Monthly teaching hours", new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ClaimId",
                keyValue: 2,
                columns: new[] { "LecturerName", "Notes", "ProcessedByName", "ProcessedDate", "SubmittedDate" },
                values: new object[] { "John Lecturer", "Research supervision", "Sarah Coordinator", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Lecturer" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah Coordinator" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mike Manager" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ClaimId",
                keyValue: 1,
                columns: new[] { "LecturerName", "Notes", "SubmittedDate" },
                values: new object[] { "Dr. John Smith", "Monthly teaching hours for August", new DateTime(2025, 11, 19, 16, 9, 17, 334, DateTimeKind.Local).AddTicks(308) });

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "ClaimId",
                keyValue: 2,
                columns: new[] { "LecturerName", "Notes", "ProcessedByName", "ProcessedDate", "SubmittedDate" },
                values: new object[] { "Dr. John Smith", "Research supervision hours", "Prof. Sarah Wilson", new DateTime(2025, 11, 20, 16, 9, 17, 334, DateTimeKind.Local).AddTicks(1658), new DateTime(2025, 11, 16, 16, 9, 17, 334, DateTimeKind.Local).AddTicks(1648) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 11, 21, 16, 9, 17, 329, DateTimeKind.Local).AddTicks(2288), "HR Administrator" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 11, 21, 16, 9, 17, 332, DateTimeKind.Local).AddTicks(2790), "Dr. John Smith" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 11, 21, 16, 9, 17, 332, DateTimeKind.Local).AddTicks(2859), "Prof. Sarah Wilson" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "FullName" },
                values: new object[] { new DateTime(2025, 11, 21, 16, 9, 17, 332, DateTimeKind.Local).AddTicks(2864), "Dr. Michael Brown" });
        }
    }
}

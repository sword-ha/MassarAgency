using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MassarAgency.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeductionPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeductionPerAbsenceDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeductionPerLateDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxMonthlyDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeductionPolicies",
                columns: new[] { "Id", "CreatedAt", "DeductionPerAbsenceDay", "DeductionPerLateDay", "IsActive", "MaxMonthlyDeduction", "Name" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 500m, 150m, true, 5000m, "Standard Deduction Policy" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Executive leadership and strategic decision-making.", "High Board" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sales operations and client acquisition.", "SALES" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Account management and client relations.", "ACCOUNT" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Social media strategy and management.", "SOCIAL MEDIA" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Graphic design and visual content creation.", "Graphic" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Video production and post-production editing.", "Video editing" },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Media planning, buying, and ad campaign management.", "Media Buying" },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Search engine optimization and organic traffic growth.", "SEO" },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Data entry, processing, and records management.", "Data Entry" },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Day-to-day business operations and logistics.", "Operation" },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Content writing, copywriting, and editorial.", "Content" },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Information technology and technical support.", "IT" },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UI/UX design and front-end development.", "UI-PROGRAMMING" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BaseSalary", "CreatedAt", "DepartmentId", "Email", "FullName", "HireDate", "IsActive", "JobTitle", "PasswordHash", "Phone", "Role", "Username" },
                values: new object[,]
                {
                    { 1, 25000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "admin@masar.com", "Admin User", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "System Administrator", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", "0550000000", 1, "admin" },
                    { 2, 15000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "ahmed@masar.com", "Ahmed Al-Rashid", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sales Manager", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0551234567", 0, "ahmed" },
                    { 3, 12000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "sara@masar.com", "Sara Mohammed", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Social Media Specialist", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0559876543", 0, "sara" },
                    { 4, 14000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "omar@masar.com", "Omar Hassan", new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Account Manager", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0553456789", 0, "omar" },
                    { 5, 11000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "fatima@masar.com", "Fatima Al-Zahrani", new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Graphic Designer", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0557654321", 0, "fatima" },
                    { 6, 16000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "khalid@masar.com", "Khalid Ibrahim", new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Operations Lead", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0552345678", 0, "khalid" },
                    { 7, 13000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "nora@masar.com", "Nora Abdullah", new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "SEO Specialist", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0558765432", 0, "nora" },
                    { 8, 10000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "youssef@masar.com", "Youssef Al-Otaibi", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Video Editor", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0554567890", 0, "youssef" },
                    { 9, 9000m, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "layla@masar.com", "Layla Mansour", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Content Writer", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0556789012", 0, "layla" },
                    { 10, 13000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "tariq@masar.com", "Tariq Saleh", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Media Buyer", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0551112233", 0, "tariq" },
                    { 11, 8000m, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "reem@masar.com", "Reem Al-Dosari", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Data Entry Clerk", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0553344556", 0, "reem" },
                    { 12, 18000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "hassan@masar.com", "Hassan Nabil", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "UI Developer", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0555566778", 0, "hassan" },
                    { 13, 30000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "mona@masar.com", "Mona Al-Harbi", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Board Member", "4D0+yNUDX4ch9dxkVG5Z7XkNvLO3tZj+VwV8zXtoOwA=", "0557788990", 0, "mona" }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "CreatedAt", "Date", "EmployeeId", "Notes", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 2, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 3, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 4, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 5, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 6, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 7, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Unexcused absence", 1 },
                    { 8, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 9, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 10, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Unexcused absence", 1 },
                    { 11, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 0 },
                    { 12, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 13, new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 14, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 15, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 16, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 17, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 18, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 19, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Unexcused absence", 1 },
                    { 20, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 21, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 22, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 23, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 24, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Unexcused absence", 1 },
                    { 25, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Unexcused absence", 1 },
                    { 26, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 27, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 28, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Unexcused absence", 1 },
                    { 29, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 30, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Arrived late", 2 },
                    { 31, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 32, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 33, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 34, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 35, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 36, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 37, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Unexcused absence", 1 },
                    { 38, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 39, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 40, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Unexcused absence", 1 },
                    { 41, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Unexcused absence", 1 },
                    { 42, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 43, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 44, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 45, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 46, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Unexcused absence", 1 },
                    { 47, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 48, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 49, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 50, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Unexcused absence", 1 },
                    { 51, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 52, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 53, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 54, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 55, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 56, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 57, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 58, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 59, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Unexcused absence", 1 },
                    { 60, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 61, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 62, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 63, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 0 },
                    { 64, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 65, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 66, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Unexcused absence", 1 },
                    { 67, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 68, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 69, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 70, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Unexcused absence", 1 },
                    { 71, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 72, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 73, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 74, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 75, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Unexcused absence", 1 },
                    { 76, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 0 },
                    { 77, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 78, new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 79, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 80, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 81, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Arrived late", 2 },
                    { 82, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Arrived late", 2 },
                    { 83, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 84, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 85, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 86, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 87, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 88, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 89, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 0 },
                    { 90, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Unexcused absence", 1 },
                    { 91, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 92, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 93, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 94, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Arrived late", 2 },
                    { 95, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 96, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Unexcused absence", 1 },
                    { 97, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 98, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Arrived late", 2 },
                    { 99, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 3 },
                    { 100, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 101, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Unexcused absence", 1 },
                    { 102, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 0 },
                    { 103, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 104, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 105, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 106, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Unexcused absence", 1 },
                    { 107, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 108, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 109, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 110, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 111, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 112, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 3 },
                    { 113, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Unexcused absence", 1 },
                    { 114, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 115, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Unexcused absence", 1 },
                    { 116, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 117, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 118, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 3 },
                    { 119, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 120, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 121, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 122, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Arrived late", 2 },
                    { 123, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 124, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 125, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 126, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 127, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 128, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, null, 0 },
                    { 129, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 130, new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 131, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 3 },
                    { 132, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 133, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 134, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 135, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 136, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Unexcused absence", 1 },
                    { 137, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Unexcused absence", 1 },
                    { 138, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 139, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 140, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Unexcused absence", 1 },
                    { 141, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Arrived late", 2 },
                    { 142, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 143, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Arrived late", 2 },
                    { 144, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Unexcused absence", 1 },
                    { 145, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 146, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 147, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 148, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 149, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 150, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 151, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 152, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 153, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 154, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Arrived late", 2 },
                    { 155, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 3 },
                    { 156, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, "Unexcused absence", 1 },
                    { 157, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 158, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Arrived late", 2 },
                    { 159, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 160, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 0 },
                    { 161, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 0 },
                    { 162, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 163, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Unexcused absence", 1 },
                    { 164, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 165, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 166, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 0 },
                    { 167, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Arrived late", 2 },
                    { 168, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 169, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 170, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 3 },
                    { 171, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 172, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 0 },
                    { 173, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Arrived late", 2 },
                    { 174, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Unexcused absence", 1 },
                    { 175, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 176, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 177, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 178, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 179, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Arrived late", 2 },
                    { 180, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Unexcused absence", 1 },
                    { 181, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 0 },
                    { 182, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 },
                    { 183, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 0 },
                    { 184, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 0 },
                    { 185, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Arrived late", 2 },
                    { 186, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 3 },
                    { 187, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Arrived late", 2 },
                    { 188, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 0 },
                    { 189, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 0 },
                    { 190, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, null, 0 },
                    { 191, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 0 },
                    { 192, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Arrived late", 2 },
                    { 193, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Unexcused absence", 1 },
                    { 194, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Unexcused absence", 1 },
                    { 195, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId_Date",
                table: "Attendances",
                columns: new[] { "EmployeeId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Username",
                table: "Employees",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "DeductionPolicies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

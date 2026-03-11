using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MassarAgency.Infrastructure.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Departments — exact company structure
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "High Board", Description = "Executive leadership and strategic decision-making.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 2, Name = "SALES", Description = "Sales operations and client acquisition.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 3, Name = "ACCOUNT", Description = "Account management and client relations.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 4, Name = "SOCIAL MEDIA", Description = "Social media strategy and management.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 5, Name = "Graphic", Description = "Graphic design and visual content creation.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 6, Name = "Video editing", Description = "Video production and post-production editing.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 7, Name = "Media Buying", Description = "Media planning, buying, and ad campaign management.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 8, Name = "SEO", Description = "Search engine optimization and organic traffic growth.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 9, Name = "Data Entry", Description = "Data entry, processing, and records management.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 10, Name = "Operation", Description = "Day-to-day business operations and logistics.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 11, Name = "Content", Description = "Content writing, copywriting, and editorial.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 12, Name = "IT", Description = "Information technology and technical support.", CreatedAt = new DateTime(2024, 1, 1) },
            new Department { Id = 13, Name = "UI-PROGRAMMING", Description = "UI/UX design and front-end development.", CreatedAt = new DateTime(2024, 1, 1) }
        );

        // Helper to hash passwords (SHA256 for seed — simple deterministic hash)
        static string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // Employees — one admin + sample employees across departments
        modelBuilder.Entity<Employee>().HasData(
            // Admin account
            new Employee { Id = 1, FullName = "Admin User", Email = "admin@masar.com", Phone = "0550000000", JobTitle = "System Administrator", BaseSalary = 25000m, HireDate = new DateTime(2022, 1, 1), DepartmentId = 12, IsActive = true, Username = "admin", PasswordHash = Hash("admin123"), Role = UserRole.Admin, CreatedAt = new DateTime(2024, 1, 1) },
            // Employees
            new Employee { Id = 2, FullName = "Ahmed Al-Rashid", Email = "ahmed@masar.com", Phone = "0551234567", JobTitle = "Sales Manager", BaseSalary = 15000m, HireDate = new DateTime(2022, 3, 15), DepartmentId = 2, IsActive = true, Username = "ahmed", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 3, FullName = "Sara Mohammed", Email = "sara@masar.com", Phone = "0559876543", JobTitle = "Social Media Specialist", BaseSalary = 12000m, HireDate = new DateTime(2023, 1, 10), DepartmentId = 4, IsActive = true, Username = "sara", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 4, FullName = "Omar Hassan", Email = "omar@masar.com", Phone = "0553456789", JobTitle = "Account Manager", BaseSalary = 14000m, HireDate = new DateTime(2022, 6, 20), DepartmentId = 3, IsActive = true, Username = "omar", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 5, FullName = "Fatima Al-Zahrani", Email = "fatima@masar.com", Phone = "0557654321", JobTitle = "Graphic Designer", BaseSalary = 11000m, HireDate = new DateTime(2023, 4, 5), DepartmentId = 5, IsActive = true, Username = "fatima", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 6, FullName = "Khalid Ibrahim", Email = "khalid@masar.com", Phone = "0552345678", JobTitle = "Operations Lead", BaseSalary = 16000m, HireDate = new DateTime(2021, 11, 1), DepartmentId = 10, IsActive = true, Username = "khalid", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 7, FullName = "Nora Abdullah", Email = "nora@masar.com", Phone = "0558765432", JobTitle = "SEO Specialist", BaseSalary = 13000m, HireDate = new DateTime(2021, 8, 15), DepartmentId = 8, IsActive = true, Username = "nora", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 8, FullName = "Youssef Al-Otaibi", Email = "youssef@masar.com", Phone = "0554567890", JobTitle = "Video Editor", BaseSalary = 10000m, HireDate = new DateTime(2023, 9, 1), DepartmentId = 6, IsActive = true, Username = "youssef", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 9, FullName = "Layla Mansour", Email = "layla@masar.com", Phone = "0556789012", JobTitle = "Content Writer", BaseSalary = 9000m, HireDate = new DateTime(2024, 1, 15), DepartmentId = 11, IsActive = true, Username = "layla", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 15) },
            new Employee { Id = 10, FullName = "Tariq Saleh", Email = "tariq@masar.com", Phone = "0551112233", JobTitle = "Media Buyer", BaseSalary = 13000m, HireDate = new DateTime(2023, 5, 1), DepartmentId = 7, IsActive = true, Username = "tariq", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 11, FullName = "Reem Al-Dosari", Email = "reem@masar.com", Phone = "0553344556", JobTitle = "Data Entry Clerk", BaseSalary = 8000m, HireDate = new DateTime(2024, 2, 1), DepartmentId = 9, IsActive = true, Username = "reem", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 2, 1) },
            new Employee { Id = 12, FullName = "Hassan Nabil", Email = "hassan@masar.com", Phone = "0555566778", JobTitle = "UI Developer", BaseSalary = 18000m, HireDate = new DateTime(2022, 9, 1), DepartmentId = 13, IsActive = true, Username = "hassan", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) },
            new Employee { Id = 13, FullName = "Mona Al-Harbi", Email = "mona@masar.com", Phone = "0557788990", JobTitle = "Board Member", BaseSalary = 30000m, HireDate = new DateTime(2021, 1, 1), DepartmentId = 1, IsActive = true, Username = "mona", PasswordHash = Hash("emp123"), Role = UserRole.Employee, CreatedAt = new DateTime(2024, 1, 1) }
        );

        // Deduction Policy
        modelBuilder.Entity<DeductionPolicy>().HasData(
            new DeductionPolicy { Id = 1, Name = "Standard Deduction Policy", DeductionPerAbsenceDay = 500m, DeductionPerLateDay = 150m, MaxMonthlyDeduction = 5000m, IsActive = true, CreatedAt = new DateTime(2024, 1, 1) }
        );

        // Attendance seed data for March 2025
        var baseDate = new DateTime(2025, 3, 1);
        int id = 1;
        var random = new Random(42);

        for (int day = 1; day <= 20; day++)
        {
            var date = baseDate.AddDays(day - 1);
            if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday)
                continue;

            for (int empId = 1; empId <= 13; empId++)
            {
                var roll = random.Next(100);
                var status = roll < 70 ? AttendanceStatus.Present
                    : roll < 85 ? AttendanceStatus.Absent
                    : roll < 95 ? AttendanceStatus.Late
                    : AttendanceStatus.Excused;

                modelBuilder.Entity<Attendance>().HasData(new Attendance
                {
                    Id = id++,
                    EmployeeId = empId,
                    Date = date,
                    Status = status,
                    Notes = status == AttendanceStatus.Absent ? "Unexcused absence"
                          : status == AttendanceStatus.Late ? "Arrived late"
                          : null,
                    CreatedAt = date
                });
            }
        }
    }
}

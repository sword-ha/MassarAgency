using System.ComponentModel.DataAnnotations;

namespace MassarAgency.Application.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job title is required.")]
    [StringLength(100)]
    [Display(Name = "Job Title")]
    public string JobTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Base salary is required.")]
    [Range(0, 1000000, ErrorMessage = "Salary must be between 0 and 1,000,000.")]
    [Display(Name = "Base Salary")]
    public decimal BaseSalary { get; set; }

    [Required(ErrorMessage = "Hire date is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Department is required.")]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50)]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
}

public class UpdateEmployeeDto : CreateEmployeeDto
{
    public int Id { get; set; }
    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "New Password (leave blank to keep current)")]
    public new string? Password { get; set; }
}

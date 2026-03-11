using System.ComponentModel.DataAnnotations;
using MassarAgency.Domain.Enums;

namespace MassarAgency.Application.DTOs;

public class AttendanceDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public AttendanceStatus Status { get; set; }
    public string StatusDisplay => Status.ToString();
    public string? Notes { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
}

public class RecordAttendanceDto
{
    [Required(ErrorMessage = "Employee is required.")]
    [Display(Name = "Employee")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Status is required.")]
    public AttendanceStatus Status { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

public class BulkAttendanceDto
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    public List<EmployeeAttendanceEntry> Entries { get; set; } = new();
}

public class EmployeeAttendanceEntry
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public AttendanceStatus Status { get; set; }
    public string? Notes { get; set; }
}

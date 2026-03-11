namespace MassarAgency.Application.DTOs;

public class DeductionSummaryDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public int AbsenceDays { get; set; }
    public int LateDays { get; set; }
    public decimal DeductionPerAbsenceDay { get; set; }
    public decimal DeductionPerLateDay { get; set; }
    public decimal AbsenceDeduction { get; set; }
    public decimal LateDeduction { get; set; }
    public decimal TotalDeduction { get; set; }
    public decimal NetSalary { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}

public class MonthlyReportDto
{
    public int Month { get; set; }
    public int Year { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public int TotalEmployees { get; set; }
    public int TotalPresent { get; set; }
    public int TotalAbsent { get; set; }
    public int TotalLate { get; set; }
    public int TotalExcused { get; set; }
    public decimal TotalDeductions { get; set; }
    public List<DeductionSummaryDto> EmployeeDeductions { get; set; } = new();
}

public class DashboardDto
{
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public int TodayPresent { get; set; }
    public int TodayAbsent { get; set; }
    public int TodayLate { get; set; }
    public decimal MonthlyDeductions { get; set; }
    public double AttendanceRate { get; set; }
    public List<DepartmentDto> RecentDepartments { get; set; } = new();
    public List<AttendanceDto> RecentAttendance { get; set; } = new();
}

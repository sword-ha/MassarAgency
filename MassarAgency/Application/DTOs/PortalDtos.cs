namespace MassarAgency.Application.DTOs;

public class EmployeePortalDashboardDto
{
    public EmployeeDto Employee { get; set; } = null!;
    public int AbsenceDaysThisMonth { get; set; }
    public int LateDaysThisMonth { get; set; }
    public int PresentDaysThisMonth { get; set; }
    public int ExcusedDaysThisMonth { get; set; }
    public decimal DeductionThisMonth { get; set; }
    public decimal NetSalaryThisMonth { get; set; }
    public int TotalAbsenceDaysAllTime { get; set; }
    public int TotalLateDaysAllTime { get; set; }
    public List<AttendanceDto> RecentAttendance { get; set; } = new();
    public int CurrentMonth { get; set; }
    public int CurrentYear { get; set; }
    public string MonthName { get; set; } = string.Empty;
}

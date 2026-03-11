using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Enums;
using MassarAgency.Domain.Interfaces;

namespace MassarAgency.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IDepartmentRepository _departmentRepo;
    private readonly IDeductionService _deductionService;

    public DashboardService(
        IEmployeeRepository employeeRepo,
        IAttendanceRepository attendanceRepo,
        IDepartmentRepository departmentRepo,
        IDeductionService deductionService)
    {
        _employeeRepo = employeeRepo;
        _attendanceRepo = attendanceRepo;
        _departmentRepo = departmentRepo;
        _deductionService = deductionService;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var employees = (await _employeeRepo.GetAllWithDepartmentAsync()).ToList();
        var departments = (await _departmentRepo.GetAllWithEmployeeCountAsync()).ToList();
        var today = DateTime.Today;
        var todayAttendance = (await _attendanceRepo.GetMonthlyReportAsync(today.Month, today.Year))
            .Where(a => a.Date.Date == today).ToList();

        var monthlyReport = await _deductionService.GetMonthlyReportAsync(today.Month, today.Year);

        var recentAttendance = (await _attendanceRepo.GetMonthlyReportAsync(today.Month, today.Year))
            .OrderByDescending(a => a.Date)
            .Take(10)
            .Select(a => new AttendanceDto
            {
                Id = a.Id,
                Date = a.Date,
                Status = a.Status,
                Notes = a.Notes,
                EmployeeId = a.EmployeeId,
                EmployeeName = a.Employee?.FullName ?? string.Empty,
                DepartmentName = a.Employee?.Department?.Name ?? string.Empty
            }).ToList();

        var totalActive = employees.Count(e => e.IsActive);

        return new DashboardDto
        {
            TotalEmployees = employees.Count,
            ActiveEmployees = totalActive,
            TotalDepartments = departments.Count,
            TodayPresent = todayAttendance.Count(a => a.Status == AttendanceStatus.Present),
            TodayAbsent = todayAttendance.Count(a => a.Status == AttendanceStatus.Absent),
            MonthlyDeductions = monthlyReport.TotalDeductions,
            AttendanceRate = totalActive > 0
                ? Math.Round((double)todayAttendance.Count(a => a.Status == AttendanceStatus.Present) / totalActive * 100, 1)
                : 0,
            RecentDepartments = departments.Take(5).Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                EmployeeCount = d.Employees.Count,
                CreatedAt = d.CreatedAt
            }).ToList(),
            RecentAttendance = recentAttendance
        };
    }
}

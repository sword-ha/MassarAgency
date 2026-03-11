using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Enums;
using MassarAgency.Domain.Interfaces;
using System.Globalization;

namespace MassarAgency.Application.Services;

public class PortalService : IPortalService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IDeductionPolicyRepository _policyRepo;

    public PortalService(
        IEmployeeRepository employeeRepo,
        IAttendanceRepository attendanceRepo,
        IDeductionPolicyRepository policyRepo)
    {
        _employeeRepo = employeeRepo;
        _attendanceRepo = attendanceRepo;
        _policyRepo = policyRepo;
    }

    public async Task<EmployeePortalDashboardDto> GetPortalDashboardAsync(int employeeId, int? month = null, int? year = null)
    {
        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        var employee = await _employeeRepo.GetByIdWithDepartmentAsync(employeeId)
            ?? throw new KeyNotFoundException("Employee not found.");

        var policy = await _policyRepo.GetActivePolicyAsync();
        var deductionPerAbsence = policy?.DeductionPerAbsenceDay ?? 0;
        var deductionPerLate = policy?.DeductionPerLateDay ?? 0;
        var maxDeduction = policy?.MaxMonthlyDeduction;

        var monthlyRecords = (await _attendanceRepo.GetByEmployeeIdAsync(employeeId, targetMonth, targetYear)).ToList();
        var allRecords = (await _attendanceRepo.GetAllByEmployeeAsync(employeeId)).ToList();

        var absenceThisMonth = monthlyRecords.Count(r => r.Status == AttendanceStatus.Absent);
        var lateThisMonth = monthlyRecords.Count(r => r.Status == AttendanceStatus.Late);
        var presentThisMonth = monthlyRecords.Count(r => r.Status == AttendanceStatus.Present);
        var excusedThisMonth = monthlyRecords.Count(r => r.Status == AttendanceStatus.Excused);

        var totalDeduction = (absenceThisMonth * deductionPerAbsence) + (lateThisMonth * deductionPerLate);
        if (maxDeduction.HasValue && totalDeduction > maxDeduction.Value)
            totalDeduction = maxDeduction.Value;

        return new EmployeePortalDashboardDto
        {
            Employee = new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                Phone = employee.Phone,
                JobTitle = employee.JobTitle,
                BaseSalary = employee.BaseSalary,
                HireDate = employee.HireDate,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name ?? string.Empty
            },
            AbsenceDaysThisMonth = absenceThisMonth,
            LateDaysThisMonth = lateThisMonth,
            PresentDaysThisMonth = presentThisMonth,
            ExcusedDaysThisMonth = excusedThisMonth,
            DeductionThisMonth = totalDeduction,
            NetSalaryThisMonth = employee.BaseSalary - totalDeduction,
            TotalAbsenceDaysAllTime = allRecords.Count(r => r.Status == AttendanceStatus.Absent),
            TotalLateDaysAllTime = allRecords.Count(r => r.Status == AttendanceStatus.Late),
            RecentAttendance = monthlyRecords.Take(15).Select(a => new AttendanceDto
            {
                Id = a.Id,
                Date = a.Date,
                Status = a.Status,
                Notes = a.Notes,
                EmployeeId = a.EmployeeId,
                EmployeeName = employee.FullName,
                DepartmentName = employee.Department?.Name ?? string.Empty
            }).ToList(),
            CurrentMonth = targetMonth,
            CurrentYear = targetYear,
            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(targetMonth)
        };
    }
}

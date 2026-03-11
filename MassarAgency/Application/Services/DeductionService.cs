using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Enums;
using MassarAgency.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace MassarAgency.Application.Services;

public class DeductionService : IDeductionService
{
    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IDeductionPolicyRepository _policyRepo;
    private readonly ILogger<DeductionService> _logger;

    public DeductionService(
        IAttendanceRepository attendanceRepo,
        IEmployeeRepository employeeRepo,
        IDeductionPolicyRepository policyRepo,
        ILogger<DeductionService> logger)
    {
        _attendanceRepo = attendanceRepo;
        _employeeRepo = employeeRepo;
        _policyRepo = policyRepo;
        _logger = logger;
    }

    public async Task<MonthlyReportDto> GetMonthlyReportAsync(int month, int year)
    {
        var policy = await _policyRepo.GetActivePolicyAsync();
        var deductionPerAbsence = policy?.DeductionPerAbsenceDay ?? 0;
        var deductionPerLate = policy?.DeductionPerLateDay ?? 0;
        var maxDeduction = policy?.MaxMonthlyDeduction;

        var employees = await _employeeRepo.GetAllWithDepartmentAsync();
        var activeEmployees = employees.Where(e => e.IsActive).ToList();

        var allAttendance = await _attendanceRepo.GetMonthlyReportAsync(month, year);
        var attendanceList = allAttendance.ToList();

        var report = new MonthlyReportDto
        {
            Month = month,
            Year = year,
            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
            TotalEmployees = activeEmployees.Count,
            TotalPresent = attendanceList.Count(a => a.Status == AttendanceStatus.Present),
            TotalAbsent = attendanceList.Count(a => a.Status == AttendanceStatus.Absent),
            TotalLate = attendanceList.Count(a => a.Status == AttendanceStatus.Late),
            TotalExcused = attendanceList.Count(a => a.Status == AttendanceStatus.Excused)
        };

        foreach (var emp in activeEmployees)
        {
            var absenceDays = attendanceList.Count(a => a.EmployeeId == emp.Id && a.Status == AttendanceStatus.Absent);
            var lateDays = attendanceList.Count(a => a.EmployeeId == emp.Id && a.Status == AttendanceStatus.Late);
            var absenceDeduction = absenceDays * deductionPerAbsence;
            var lateDeduction = lateDays * deductionPerLate;
            var totalDeduction = absenceDeduction + lateDeduction;

            if (maxDeduction.HasValue && totalDeduction > maxDeduction.Value)
                totalDeduction = maxDeduction.Value;

            var summary = new DeductionSummaryDto
            {
                EmployeeId = emp.Id,
                EmployeeName = emp.FullName,
                DepartmentName = emp.Department?.Name ?? string.Empty,
                BaseSalary = emp.BaseSalary,
                AbsenceDays = absenceDays,
                LateDays = lateDays,
                DeductionPerAbsenceDay = deductionPerAbsence,
                DeductionPerLateDay = deductionPerLate,
                AbsenceDeduction = absenceDeduction,
                LateDeduction = lateDeduction,
                TotalDeduction = totalDeduction,
                NetSalary = emp.BaseSalary - totalDeduction,
                Month = month,
                Year = year
            };

            report.EmployeeDeductions.Add(summary);
        }

        report.TotalDeductions = report.EmployeeDeductions.Sum(d => d.TotalDeduction);
        return report;
    }

    public async Task<DeductionSummaryDto?> GetEmployeeDeductionAsync(int employeeId, int month, int year)
    {
        var employee = await _employeeRepo.GetByIdWithDepartmentAsync(employeeId);
        if (employee is null) return null;

        var policy = await _policyRepo.GetActivePolicyAsync();
        var deductionPerAbsence = policy?.DeductionPerAbsenceDay ?? 0;
        var deductionPerLate = policy?.DeductionPerLateDay ?? 0;
        var maxDeduction = policy?.MaxMonthlyDeduction;

        var absenceDays = await _attendanceRepo.GetAbsenceDaysCountAsync(employeeId, month, year);
        var lateDays = await _attendanceRepo.GetLateDaysCountAsync(employeeId, month, year);
        var absenceDeduction = absenceDays * deductionPerAbsence;
        var lateDeduction = lateDays * deductionPerLate;
        var totalDeduction = absenceDeduction + lateDeduction;

        if (maxDeduction.HasValue && totalDeduction > maxDeduction.Value)
            totalDeduction = maxDeduction.Value;

        return new DeductionSummaryDto
        {
            EmployeeId = employee.Id,
            EmployeeName = employee.FullName,
            DepartmentName = employee.Department?.Name ?? string.Empty,
            BaseSalary = employee.BaseSalary,
            AbsenceDays = absenceDays,
            LateDays = lateDays,
            DeductionPerAbsenceDay = deductionPerAbsence,
            DeductionPerLateDay = deductionPerLate,
            AbsenceDeduction = absenceDeduction,
            LateDeduction = lateDeduction,
            TotalDeduction = totalDeduction,
            NetSalary = employee.BaseSalary - totalDeduction,
            Month = month,
            Year = year
        };
    }
}

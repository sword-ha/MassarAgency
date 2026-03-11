using MassarAgency.Application.DTOs;

namespace MassarAgency.Application.Interfaces;

public interface IDeductionService
{
    Task<MonthlyReportDto> GetMonthlyReportAsync(int month, int year);
    Task<DeductionSummaryDto?> GetEmployeeDeductionAsync(int employeeId, int month, int year);
}

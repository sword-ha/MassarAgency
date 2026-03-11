using MassarAgency.Domain.Entities;

namespace MassarAgency.Domain.Interfaces;

public interface IAttendanceRepository : IRepository<Attendance>
{
    Task<IEnumerable<Attendance>> GetByEmployeeIdAsync(int employeeId, int? month = null, int? year = null);
    Task<IEnumerable<Attendance>> GetAllByEmployeeAsync(int employeeId);
    Task<IEnumerable<Attendance>> GetMonthlyReportAsync(int month, int year);
    Task<Attendance?> GetByEmployeeDateAsync(int employeeId, DateTime date);
    Task<int> GetAbsenceDaysCountAsync(int employeeId, int month, int year);
    Task<int> GetLateDaysCountAsync(int employeeId, int month, int year);
}

using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Enums;
using MassarAgency.Domain.Interfaces;
using MassarAgency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MassarAgency.Infrastructure.Repositories;

public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
{
    public AttendanceRepository(MasarDbContext context) : base(context) { }

    public async Task<IEnumerable<Attendance>> GetByEmployeeIdAsync(int employeeId, int? month = null, int? year = null)
    {
        var query = _dbSet
            .Include(a => a.Employee)
            .ThenInclude(e => e.Department)
            .Where(a => a.EmployeeId == employeeId);

        if (month.HasValue)
            query = query.Where(a => a.Date.Month == month.Value);
        if (year.HasValue)
            query = query.Where(a => a.Date.Year == year.Value);

        return await query.OrderByDescending(a => a.Date).ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> GetAllByEmployeeAsync(int employeeId)
    {
        return await _dbSet
            .Include(a => a.Employee)
            .ThenInclude(e => e.Department)
            .Where(a => a.EmployeeId == employeeId)
            .OrderByDescending(a => a.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> GetMonthlyReportAsync(int month, int year)
    {
        return await _dbSet
            .Include(a => a.Employee)
            .ThenInclude(e => e.Department)
            .Where(a => a.Date.Month == month && a.Date.Year == year)
            .OrderByDescending(a => a.Date)
            .ToListAsync();
    }

    public async Task<Attendance?> GetByEmployeeDateAsync(int employeeId, DateTime date)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date.Date == date.Date);
    }

    public async Task<int> GetAbsenceDaysCountAsync(int employeeId, int month, int year)
    {
        return await _dbSet.CountAsync(a =>
            a.EmployeeId == employeeId &&
            a.Date.Month == month &&
            a.Date.Year == year &&
            a.Status == AttendanceStatus.Absent);
    }

    public async Task<int> GetLateDaysCountAsync(int employeeId, int month, int year)
    {
        return await _dbSet.CountAsync(a =>
            a.EmployeeId == employeeId &&
            a.Date.Month == month &&
            a.Date.Year == year &&
            a.Status == AttendanceStatus.Late);
    }
}

using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Enums;
using MassarAgency.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MassarAgency.Application.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AttendanceService> _logger;

    public AttendanceService(
        IAttendanceRepository attendanceRepo,
        IEmployeeRepository employeeRepo,
        IUnitOfWork unitOfWork,
        ILogger<AttendanceService> logger)
    {
        _attendanceRepo = attendanceRepo;
        _employeeRepo = employeeRepo;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task RecordAttendanceAsync(RecordAttendanceDto dto)
    {
        var existing = await _attendanceRepo.GetByEmployeeDateAsync(dto.EmployeeId, dto.Date);
        if (existing is not null)
        {
            existing.Status = dto.Status;
            existing.Notes = dto.Notes;
            _attendanceRepo.Update(existing);
        }
        else
        {
            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                Date = dto.Date.Date,
                Status = dto.Status,
                Notes = dto.Notes
            };
            await _attendanceRepo.AddAsync(attendance);
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Attendance recorded for Employee {EmpId} on {Date}.", dto.EmployeeId, dto.Date.ToShortDateString());
    }

    public async Task RecordBulkAttendanceAsync(BulkAttendanceDto dto)
    {
        foreach (var entry in dto.Entries)
        {
            var existing = await _attendanceRepo.GetByEmployeeDateAsync(entry.EmployeeId, dto.Date);
            if (existing is not null)
            {
                existing.Status = entry.Status;
                existing.Notes = entry.Notes;
                _attendanceRepo.Update(existing);
            }
            else
            {
                var attendance = new Attendance
                {
                    EmployeeId = entry.EmployeeId,
                    Date = dto.Date.Date,
                    Status = entry.Status,
                    Notes = entry.Notes
                };
                await _attendanceRepo.AddAsync(attendance);
            }
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Bulk attendance recorded for {Date} with {Count} entries.", dto.Date.ToShortDateString(), dto.Entries.Count);
    }

    public async Task<IEnumerable<AttendanceDto>> GetByEmployeeAsync(int employeeId, int? month = null, int? year = null)
    {
        var records = await _attendanceRepo.GetByEmployeeIdAsync(employeeId, month, year);
        return records.Select(a => new AttendanceDto
        {
            Id = a.Id,
            Date = a.Date,
            Status = a.Status,
            Notes = a.Notes,
            EmployeeId = a.EmployeeId,
            EmployeeName = a.Employee?.FullName ?? string.Empty,
            DepartmentName = a.Employee?.Department?.Name ?? string.Empty
        });
    }

    public async Task<BulkAttendanceDto> PrepareBulkAttendanceAsync(DateTime date)
    {
        var employees = await _employeeRepo.GetAllWithDepartmentAsync();
        var activeEmployees = employees.Where(e => e.IsActive).ToList();

        var dto = new BulkAttendanceDto { Date = date };

        foreach (var emp in activeEmployees)
        {
            var existing = await _attendanceRepo.GetByEmployeeDateAsync(emp.Id, date);
            dto.Entries.Add(new EmployeeAttendanceEntry
            {
                EmployeeId = emp.Id,
                EmployeeName = emp.FullName,
                DepartmentName = emp.Department?.Name ?? string.Empty,
                Status = existing?.Status ?? AttendanceStatus.Present,
                Notes = existing?.Notes
            });
        }

        return dto;
    }
}

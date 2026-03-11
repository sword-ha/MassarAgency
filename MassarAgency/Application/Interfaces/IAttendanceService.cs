using MassarAgency.Application.DTOs;

namespace MassarAgency.Application.Interfaces;

public interface IAttendanceService
{
    Task RecordAttendanceAsync(RecordAttendanceDto dto);
    Task RecordBulkAttendanceAsync(BulkAttendanceDto dto);
    Task<IEnumerable<AttendanceDto>> GetByEmployeeAsync(int employeeId, int? month = null, int? year = null);
    Task<BulkAttendanceDto> PrepareBulkAttendanceAsync(DateTime date);
}

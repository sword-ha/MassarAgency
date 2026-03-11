using MassarAgency.Application.DTOs;

namespace MassarAgency.Application.Interfaces;

public interface IPortalService
{
    Task<EmployeePortalDashboardDto> GetPortalDashboardAsync(int employeeId, int? month = null, int? year = null);
}

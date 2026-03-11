using MassarAgency.Application.DTOs;

namespace MassarAgency.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}

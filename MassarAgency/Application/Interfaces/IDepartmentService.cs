using MassarAgency.Application.DTOs;

namespace MassarAgency.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateDepartmentDto dto);
    Task UpdateAsync(UpdateDepartmentDto dto);
    Task DeleteAsync(int id);
}

using MassarAgency.Domain.Entities;

namespace MassarAgency.Domain.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByIdWithEmployeesAsync(int id);
    Task<IEnumerable<Department>> GetAllWithEmployeeCountAsync();
}

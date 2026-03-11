using MassarAgency.Domain.Entities;

namespace MassarAgency.Domain.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetAllWithDepartmentAsync();
    Task<Employee?> GetByIdWithDepartmentAsync(int id);
    Task<IEnumerable<Employee>> SearchAsync(string searchTerm);
    Task<(IEnumerable<Employee> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? searchTerm = null, int? departmentId = null);
    Task<Employee?> GetByUsernameAsync(string username);
    Task<Employee?> GetByUsernameOrEmailAsync(string usernameOrEmail);
}

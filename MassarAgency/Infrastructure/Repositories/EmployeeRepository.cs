using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Interfaces;
using MassarAgency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MassarAgency.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(MasarDbContext context) : base(context) { }

    public async Task<IEnumerable<Employee>> GetAllWithDepartmentAsync()
        => await _dbSet.Include(e => e.Department).OrderBy(e => e.FullName).ToListAsync();

    public async Task<Employee?> GetByIdWithDepartmentAsync(int id)
        => await _dbSet.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

    public async Task<Employee?> GetByUsernameAsync(string username)
        => await _dbSet.Include(e => e.Department).FirstOrDefaultAsync(e => e.Username == username);

    public async Task<Employee?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        => await _dbSet.Include(e => e.Department).FirstOrDefaultAsync(e => e.Username == usernameOrEmail || e.Email == usernameOrEmail);

    public async Task<IEnumerable<Employee>> SearchAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await _dbSet
            .Include(e => e.Department)
            .Where(e => e.FullName.ToLower().Contains(term)
                     || e.Email.ToLower().Contains(term)
                     || e.JobTitle.ToLower().Contains(term)
                     || e.Department.Name.ToLower().Contains(term))
            .OrderBy(e => e.FullName)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Employee> Items, int TotalCount)> GetPagedAsync(
        int page, int pageSize, string? searchTerm = null, int? departmentId = null)
    {
        var query = _dbSet.Include(e => e.Department).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.ToLower();
            query = query.Where(e => e.FullName.ToLower().Contains(term)
                                  || e.Email.ToLower().Contains(term)
                                  || e.JobTitle.ToLower().Contains(term));
        }

        if (departmentId.HasValue)
            query = query.Where(e => e.DepartmentId == departmentId.Value);

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(e => e.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}

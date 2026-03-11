using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Interfaces;
using MassarAgency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MassarAgency.Infrastructure.Repositories;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(MasarDbContext context) : base(context) { }

    public async Task<Department?> GetByIdWithEmployeesAsync(int id)
        => await _dbSet.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == id);

    public async Task<IEnumerable<Department>> GetAllWithEmployeeCountAsync()
        => await _dbSet.Include(d => d.Employees).OrderBy(d => d.Name).ToListAsync();
}

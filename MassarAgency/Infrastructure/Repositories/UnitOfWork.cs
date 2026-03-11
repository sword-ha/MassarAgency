using MassarAgency.Domain.Interfaces;
using MassarAgency.Infrastructure.Data;

namespace MassarAgency.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MasarDbContext _context;

    public UnitOfWork(MasarDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}

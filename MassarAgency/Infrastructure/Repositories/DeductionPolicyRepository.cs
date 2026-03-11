using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Interfaces;
using MassarAgency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MassarAgency.Infrastructure.Repositories;

public class DeductionPolicyRepository : Repository<DeductionPolicy>, IDeductionPolicyRepository
{
    public DeductionPolicyRepository(MasarDbContext context) : base(context) { }

    public async Task<DeductionPolicy?> GetActivePolicyAsync()
        => await _dbSet.FirstOrDefaultAsync(p => p.IsActive);
}

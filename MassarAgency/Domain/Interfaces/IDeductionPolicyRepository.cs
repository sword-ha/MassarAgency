using MassarAgency.Domain.Entities;

namespace MassarAgency.Domain.Interfaces;

public interface IDeductionPolicyRepository : IRepository<DeductionPolicy>
{
    Task<DeductionPolicy?> GetActivePolicyAsync();
}

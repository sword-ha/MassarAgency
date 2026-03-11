using MassarAgency.Application.DTOs;

namespace MassarAgency.Application.Interfaces;

public interface IDeductionPolicyService
{
    Task<DeductionPolicyDto?> GetActivePolicyAsync();
    Task<IEnumerable<DeductionPolicyDto>> GetAllAsync();
    Task<DeductionPolicyDto?> GetByIdAsync(int id);
    Task UpdateAsync(UpdateDeductionPolicyDto dto);
}

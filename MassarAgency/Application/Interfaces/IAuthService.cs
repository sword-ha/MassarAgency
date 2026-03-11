using MassarAgency.Application.DTOs;
using MassarAgency.Domain.Entities;

namespace MassarAgency.Application.Interfaces;

public interface IAuthService
{
    Task<Employee?> ValidateLoginAsync(string usernameOrEmail, string password);
}

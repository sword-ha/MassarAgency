using System.Security.Cryptography;
using System.Text;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Interfaces;

namespace MassarAgency.Application.Services;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepo;

    public AuthService(IEmployeeRepository employeeRepo)
    {
        _employeeRepo = employeeRepo;
    }

    public async Task<Employee?> ValidateLoginAsync(string usernameOrEmail, string password)
    {
        var employee = await _employeeRepo.GetByUsernameOrEmailAsync(usernameOrEmail);
        if (employee is null || !employee.IsActive)
            return null;

        var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        return employee.PasswordHash == hash ? employee : null;
    }
}

using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Enums;
using MassarAgency.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace MassarAgency.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IDepartmentRepository _departmentRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo, IUnitOfWork unitOfWork, ILogger<EmployeeService> logger)
    {
        _employeeRepo = employeeRepo;
        _departmentRepo = departmentRepo;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PagedResult<EmployeeDto>> GetPagedAsync(int page, int pageSize, string? searchTerm = null, int? departmentId = null)
    {
        var (items, totalCount) = await _employeeRepo.GetPagedAsync(page, pageSize, searchTerm, departmentId);
        return new PagedResult<EmployeeDto>
        {
            Items = items.Select(MapToDto),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _employeeRepo.GetAllWithDepartmentAsync();
        return employees.Select(MapToDto);
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _employeeRepo.GetByIdWithDepartmentAsync(id);
        return employee is null ? null : MapToDto(employee);
    }

    public async Task CreateAsync(CreateEmployeeDto dto)
    {
        var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
        var role = department?.Name == "High Board" ? UserRole.Admin : UserRole.Employee;

        var employee = new Employee
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            JobTitle = dto.JobTitle,
            BaseSalary = dto.BaseSalary,
            HireDate = dto.HireDate,
            DepartmentId = dto.DepartmentId,
            IsActive = true,
            Username = dto.Username,
            PasswordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(dto.Password))),
            Role = role
        };

        await _employeeRepo.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Employee {Name} created with ID {Id}.", employee.FullName, employee.Id);
    }

    public async Task UpdateAsync(UpdateEmployeeDto dto)
    {
        var employee = await _employeeRepo.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Employee with ID {dto.Id} not found.");

        var department = await _departmentRepo.GetByIdAsync(dto.DepartmentId);
        var role = department?.Name == "High Board" ? UserRole.Admin : UserRole.Employee;

        employee.FullName = dto.FullName;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;
        employee.JobTitle = dto.JobTitle;
        employee.BaseSalary = dto.BaseSalary;
        employee.HireDate = dto.HireDate;
        employee.DepartmentId = dto.DepartmentId;
        employee.IsActive = dto.IsActive;
        employee.Username = dto.Username;
        employee.Role = role;

        if (!string.IsNullOrWhiteSpace(dto.Password))
            employee.PasswordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(dto.Password)));

        _employeeRepo.Update(employee);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Employee {Id} updated.", dto.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _employeeRepo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Employee with ID {id} not found.");

        _employeeRepo.Remove(employee);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Employee {Id} deleted.", id);
    }

    private static EmployeeDto MapToDto(Employee e) => new()
    {
        Id = e.Id,
        FullName = e.FullName,
        Email = e.Email,
        Phone = e.Phone,
        JobTitle = e.JobTitle,
        BaseSalary = e.BaseSalary,
        HireDate = e.HireDate,
        IsActive = e.IsActive,
        DepartmentId = e.DepartmentId,
        DepartmentName = e.Department?.Name ?? string.Empty,
        Username = e.Username
    };
}

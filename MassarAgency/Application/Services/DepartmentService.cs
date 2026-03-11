using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using MassarAgency.Domain.Entities;
using MassarAgency.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MassarAgency.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IDepartmentRepository departmentRepo, IUnitOfWork unitOfWork, ILogger<DepartmentService> logger)
    {
        _departmentRepo = departmentRepo;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        var departments = await _departmentRepo.GetAllWithEmployeeCountAsync();
        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            EmployeeCount = d.Employees.Count,
            CreatedAt = d.CreatedAt
        });
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _departmentRepo.GetByIdWithEmployeesAsync(id);
        if (department is null) return null;

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            EmployeeCount = department.Employees.Count,
            CreatedAt = department.CreatedAt
        };
    }

    public async Task CreateAsync(CreateDepartmentDto dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _departmentRepo.AddAsync(department);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Department {Name} created.", department.Name);
    }

    public async Task UpdateAsync(UpdateDepartmentDto dto)
    {
        var department = await _departmentRepo.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException($"Department with ID {dto.Id} not found.");

        department.Name = dto.Name;
        department.Description = dto.Description;

        _departmentRepo.Update(department);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Department {Id} updated.", dto.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _departmentRepo.GetByIdWithEmployeesAsync(id)
            ?? throw new KeyNotFoundException($"Department with ID {id} not found.");

        if (department.Employees.Count != 0)
            throw new InvalidOperationException("Cannot delete a department that has employees.");

        _departmentRepo.Remove(department);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Department {Id} deleted.", id);
    }
}

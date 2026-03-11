using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MassarAgency.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, IDepartmentService departmentService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null, int? departmentId = null)
    {
        try
        {
            var result = await _employeeService.GetPagedAsync(page, 10, search, departmentId);
            var departments = await _departmentService.GetAllAsync();

            ViewBag.Search = search;
            ViewBag.DepartmentId = departmentId;
            ViewBag.Departments = new SelectList(departments, "Id", "Name", departmentId);

            return View(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading employees list.");
            TempData["Error"] = "An error occurred while loading employees.";
            return View(new PagedResult<EmployeeDto>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee is null)
        {
            TempData["Error"] = "Employee not found.";
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    public async Task<IActionResult> Create()
    {
        await LoadDepartments();
        return View(new CreateEmployeeDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDepartments();
            return View(dto);
        }

        try
        {
            await _employeeService.CreateAsync(dto);
            TempData["Success"] = "Employee created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employee.");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the employee.");
            await LoadDepartments();
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee is null)
        {
            TempData["Error"] = "Employee not found.";
            return RedirectToAction(nameof(Index));
        }

        var dto = new UpdateEmployeeDto
        {
            Id = employee.Id,
            FullName = employee.FullName,
            Email = employee.Email,
            Phone = employee.Phone,
            JobTitle = employee.JobTitle,
            BaseSalary = employee.BaseSalary,
            HireDate = employee.HireDate,
            DepartmentId = employee.DepartmentId,
            IsActive = employee.IsActive,
            Username = employee.Username
        };

        await LoadDepartments();
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadDepartments();
            return View(dto);
        }

        try
        {
            await _employeeService.UpdateAsync(dto);
            TempData["Success"] = "Employee updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            TempData["Error"] = "Employee not found.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee {Id}.", dto.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the employee.");
            await LoadDepartments();
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _employeeService.DeleteAsync(id);
            TempData["Success"] = "Employee deleted successfully.";
        }
        catch (KeyNotFoundException)
        {
            TempData["Error"] = "Employee not found.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting employee {Id}.", id);
            TempData["Error"] = "An error occurred while deleting the employee.";
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDepartments()
    {
        var departments = await _departmentService.GetAllAsync();
        ViewBag.Departments = new SelectList(departments, "Id", "Name");
    }
}

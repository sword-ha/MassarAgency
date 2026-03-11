using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MassarAgency.Controllers;

[Authorize(Roles = "Admin")]
public class DepartmentsController : Controller
{
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var departments = await _departmentService.GetAllAsync();
            return View(departments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading departments.");
            TempData["Error"] = "An error occurred while loading departments.";
            return View(Enumerable.Empty<DepartmentDto>());
        }
    }

    public IActionResult Create()
    {
        return View(new CreateDepartmentDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDepartmentDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            await _departmentService.CreateAsync(dto);
            TempData["Success"] = "Department created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating department.");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the department.");
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var department = await _departmentService.GetByIdAsync(id);
        if (department is null)
        {
            TempData["Error"] = "Department not found.";
            return RedirectToAction(nameof(Index));
        }

        var dto = new UpdateDepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateDepartmentDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            await _departmentService.UpdateAsync(dto);
            TempData["Success"] = "Department updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            TempData["Error"] = "Department not found.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating department {Id}.", dto.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the department.");
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _departmentService.DeleteAsync(id);
            TempData["Success"] = "Department deleted successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        catch (KeyNotFoundException)
        {
            TempData["Error"] = "Department not found.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting department {Id}.", id);
            TempData["Error"] = "An error occurred while deleting the department.";
        }
        return RedirectToAction(nameof(Index));
    }
}

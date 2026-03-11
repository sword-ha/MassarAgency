using MassarAgency.Application.DTOs;
using MassarAgency.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MassarAgency.Controllers;

[Authorize(Roles = "Admin")]
public class AttendanceController : Controller
{
    private readonly IAttendanceService _attendanceService;
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<AttendanceController> _logger;

    public AttendanceController(IAttendanceService attendanceService, IEmployeeService employeeService, ILogger<AttendanceController> logger)
    {
        _attendanceService = attendanceService;
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(DateTime? date = null)
    {
        var targetDate = date ?? DateTime.Today;
        try
        {
            var bulkDto = await _attendanceService.PrepareBulkAttendanceAsync(targetDate);
            return View(bulkDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading attendance page.");
            TempData["Error"] = "An error occurred while loading attendance.";
            return View(new BulkAttendanceDto { Date = targetDate });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveBulk(BulkAttendanceDto dto)
    {
        try
        {
            await _attendanceService.RecordBulkAttendanceAsync(dto);
            TempData["Success"] = $"Attendance for {dto.Date:yyyy-MM-dd} saved successfully.";
            return RedirectToAction(nameof(Index), new { date = dto.Date.ToString("yyyy-MM-dd") });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving bulk attendance.");
            TempData["Error"] = "An error occurred while saving attendance.";
            return RedirectToAction(nameof(Index), new { date = dto.Date.ToString("yyyy-MM-dd") });
        }
    }

    public async Task<IActionResult> EmployeeHistory(int id, int? month = null, int? year = null)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee is null)
        {
            TempData["Error"] = "Employee not found.";
            return RedirectToAction(nameof(Index));
        }

        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        var records = await _attendanceService.GetByEmployeeAsync(id, targetMonth, targetYear);

        ViewBag.Employee = employee;
        ViewBag.Month = targetMonth;
        ViewBag.Year = targetYear;

        return View(records);
    }
}

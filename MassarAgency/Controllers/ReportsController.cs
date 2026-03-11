using MassarAgency.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MassarAgency.Controllers;

[Authorize(Roles = "Admin")]
public class ReportsController : Controller
{
    private readonly IDeductionService _deductionService;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(IDeductionService deductionService, ILogger<ReportsController> logger)
    {
        _deductionService = deductionService;
        _logger = logger;
    }

    public async Task<IActionResult> Monthly(int? month, int? year)
    {
        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        try
        {
            var report = await _deductionService.GetMonthlyReportAsync(targetMonth, targetYear);
            return View(report);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading monthly report.");
            TempData["Error"] = "An error occurred while generating the report.";
            return View();
        }
    }

    public async Task<IActionResult> EmployeeDeduction(int id, int? month, int? year)
    {
        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        try
        {
            var deduction = await _deductionService.GetEmployeeDeductionAsync(id, targetMonth, targetYear);
            if (deduction is null)
            {
                TempData["Error"] = "Employee not found.";
                return RedirectToAction(nameof(Monthly));
            }

            return View(deduction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading employee deduction.");
            TempData["Error"] = "An error occurred while loading deduction details.";
            return RedirectToAction(nameof(Monthly));
        }
    }
}

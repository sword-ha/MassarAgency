using System.Security.Claims;
using MassarAgency.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MassarAgency.Controllers;

[Authorize]
public class PortalController : Controller
{
    private readonly IPortalService _portalService;
    private readonly ILogger<PortalController> _logger;

    public PortalController(IPortalService portalService, ILogger<PortalController> logger)
    {
        _portalService = portalService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int? month, int? year)
    {
        var employeeIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(employeeIdClaim) || !int.TryParse(employeeIdClaim, out var employeeId))
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var dashboard = await _portalService.GetPortalDashboardAsync(employeeId, month, year);
            return View(dashboard);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading employee portal.");
            TempData["Error"] = "An error occurred while loading your dashboard.";
            return View();
        }
    }
}

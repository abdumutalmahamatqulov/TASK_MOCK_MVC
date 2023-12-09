using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TASK_MOCK_MVC.Services.Interfaces;

namespace TASK_MOCK_MVC.Controllers;
[Authorize(Roles = "ADMIN")]
public class AuditViewController : Controller
{
    private readonly IAuditRepository _context;
    public AuditViewController(IAuditRepository context) => _context = context;
    public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string Name) => View(await _context.Index(fromDate, toDate, Name));
}

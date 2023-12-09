using TASK_MOCK_MVC.Dto_s;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.ViewModels;

namespace TASK_MOCK_MVC.Services.Interfaces;
public interface IAuditRepository
{
    Task<AuditLogViewModel> Index(DateTime? fromDate, DateTime? toDate, string name);
    Task <List <AuditLog>> SortByUserName(string name);
    Task<List<AuditLog>> GetFiltered(string fromDate, string toDate);
    Task<List<AuditLog>> GetAllAudit();
}

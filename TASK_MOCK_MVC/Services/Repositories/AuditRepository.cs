﻿using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TASK_MOCK_MVC.Data;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.ExensionFuntions;
using TASK_MOCK_MVC.Services.Interfaces;
using TASK_MOCK_MVC.ViewModels;

namespace TASK_MOCK_MVC.Services.Repositories;

public class AuditRepository : IAuditRepository
{
    private readonly AppDbContext _dbContext;

    public AuditRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<AuditLogViewModel> Index(DateTime? fromDate, DateTime? toDate, string Name)
    {
        var auditlogs = await _dbContext.AuditLog.ToListAsync();

        var filteredlogs = ForAudit.FilterAuditLogsByDate(auditlogs, fromDate, toDate,Name);

        var viewModel = new AuditLogViewModel
        {
            FromDate = fromDate ?? DateTime.Today.AddDays(-100),
            ToDate = toDate ?? DateTime.Today,
            FilteredLogs = filteredlogs
        };
        return viewModel;
    }
    public async  Task<List<AuditLog>> SortByUserName(string name)
    {
        var auditlogs = _dbContext.AuditLog
            .AsEnumerable()
            .Where(log => log.UserName.Equals(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return auditlogs;
    }
    public async Task<List<AuditLog>> GetFiltered(string fromDate, string toDate)
    {
        var dateFormat = "dd.MM.yyyy";
        if(!DateTime.TryParseExact(fromDate, dateFormat, CultureInfo.InvariantCulture,DateTimeStyles.None,out var fromDateParsed))
        {
            if (fromDate != null)
                throw new Exception("Invalid date format. fromDate Forexample :dd.mm.yyyy");
        }
        if (!DateTime.TryParseExact(toDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var toDateParsed))
        {
            if (toDate != null)
                throw new Exception("Invalid date format. toDate For example : dd.mm.yyyy");

        }
        fromDateParsed = DateTime.SpecifyKind(fromDateParsed,DateTimeKind.Utc);
        toDateParsed = DateTime.SpecifyKind(toDateParsed,DateTimeKind.Utc);

        if (toDate != null)
        {
            if (fromDateParsed.Date > toDateParsed.Date)
                throw new Exception("To Date can not be before From Date.");
        }
        var auditLogs = await _dbContext.AuditLog
            .Where(log => 
                (fromDateParsed == DateTime.MinValue||log.DateTime >= fromDateParsed)&&
                (toDateParsed == DateTime.MinValue ||log.DateTime <= toDateParsed))
            .ToListAsync();

        return auditLogs;
    }

    public async Task<List<AuditLog>> GetAllAudit()
    {
        var auditLogs = await _dbContext.AuditLog.ToListAsync(); return auditLogs;
    }


}

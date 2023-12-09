using TASK_MOCK_MVC.Data;
using System.Security.Claims;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.Services.Interfaces;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace TASK_MOCK_MVC.Services.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
	public TaskRepository(AppDbContext context) => _context = context;
	public async Task<List<TaskModel>> GetAllTasksAsync() => await _context.TaskModels.ToListAsync();
    public async Task<TaskModel> CheckTaskName(TaskModel task)
    {
        var checkbase = await _context.TaskModels.FindAsync(task);
        return checkbase ?? new TaskModel();
    }

    public async Task<TaskModel> CreateAudit(TaskModel newValue, TaskModel oldValue, string actionType, User user)
    {
        var auditTrailRecord = new AuditLog
        {
            UserName = user.UserName,
            Action = actionType,
            ControllerName = "TaskModel",
            DateTime = DateTime.UtcNow,
            OldValue = JsonConvert.SerializeObject(oldValue, Formatting.Indented),
            NewValue = JsonConvert.SerializeObject(newValue, Formatting.Indented)
        };

        _context.AuditLog.Add(auditTrailRecord);
        try
        {
            await _context.SaveChangesAsync();
            return newValue;
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving audit log.", ex);
        }
    }

    public async Task<TaskModel> CreateTaskAsync(TaskModel task, string email)
    {
        _context.TaskModels.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskModel> DeleteTaskAsync(int taskId)
    {
        var taskModel = await _context.TaskModels.FirstOrDefaultAsync(x=>x.Id==taskId);
        _context.TaskModels.Remove(taskModel);
        await _context.SaveChangesAsync();
        return taskModel;
    }


    public async Task<TaskModel> GetOldValueAsync(int id)
    {
        return await _context.TaskModels.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<TaskModel> GetTaskByIdAsync(int taskId)
    {
        return await _context.TaskModels.FirstOrDefaultAsync(x => x.Id == taskId);
    }

    public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
    {
        var taskupdate = await _context.TaskModels.FirstOrDefaultAsync(x=>x.Id==task.Id);
        if (taskupdate != null)
        {
            taskupdate.Title=task.Title;    
            taskupdate.Status=task.Status;
            taskupdate.DueDate=task.DueDate;
            taskupdate.Description=task.Description;

            await _context.SaveChangesAsync();
        }

        return taskupdate;
    }
}

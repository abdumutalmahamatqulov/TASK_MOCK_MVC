using TASK_MOCK_MVC.Entities;

namespace TASK_MOCK_MVC.Services.Interfaces;

public interface ITaskRepository
{
    Task<TaskModel> GetTaskByIdAsync(int taskId);
    Task<List<TaskModel>> GetAllTasksAsync();
    Task<TaskModel> CreateTaskAsync(TaskModel task, string email);
    Task<TaskModel> UpdateTaskAsync(TaskModel task);
    Task<TaskModel> DeleteTaskAsync(int taskId);
    public Task<TaskModel> GetOldValueAsync(int id);
  
    public Task<TaskModel> CreateAudit(TaskModel newValue, TaskModel oldValue, string actionType, User user);

    Task<TaskModel> CheckTaskName(TaskModel task);
}

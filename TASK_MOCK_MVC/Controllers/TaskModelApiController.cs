using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.Services.Interfaces;

namespace TASK_MOCK_MVC.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "ADMIN")]
public class TaskModelApiController : ControllerBase
{
    private ITaskRepository _taskRepository;
    private UserManager<User> _manager;

    public TaskModelApiController(UserManager<User> manager, ITaskRepository taskRepository)
    {
        _manager = manager;
        _taskRepository = taskRepository;
    }
    [HttpGet("All")]
    public async Task<IActionResult> Index() => Ok(await _taskRepository.GetAllTasksAsync());
    [HttpGet]
    public async Task <IActionResult>Create(TaskModel model,string email)
    {
        model.DueDate = DateTime.SpecifyKind(model.DueDate, DateTimeKind.Utc);
        DateTime Limetdate = new DateTime(2050, 1, 1);
        var oldtime = DateTime.UtcNow;
        if (model.DueDate < Limetdate && model.DueDate < oldtime)
        {
            throw new Exception("Please Enter again date");
        }
        var user = await _manager.GetUserAsync(HttpContext.User);
        await _taskRepository.CreateTaskAsync(model, email);
        await _taskRepository.CreateAudit(model, null, "Create", user);
        return Ok();
    }
    [HttpPut]
    public async Task <IActionResult >Edit(int id, TaskModel model)
    {
        if (id != model.Id)
            return NotFound();
        var user = await _manager.GetUserAsync(HttpContext.User);
        var oldTask = await _taskRepository.GetOldValueAsync(id);
        var newTask = await _taskRepository.UpdateTaskAsync(model);
        await _taskRepository.CreateAudit(newTask, oldTask, "Edit", user);
        return Ok(newTask);
    }
    [HttpDelete]
    public async Task <IActionResult >Delete(int id)
    {
        var task = await _taskRepository.DeleteTaskAsync(id);
        if (task == null) return NotFound();

        var user = await _manager.GetUserAsync(HttpContext.User);
        await _taskRepository.CreateAudit(task, null, "Delete", user);
        return Ok();
    }
}

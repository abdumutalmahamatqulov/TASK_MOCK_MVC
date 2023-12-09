using System.Net.Mail;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using NToastNotify;
using TASK_MOCK_MVC.Data;
using TASK_MOCK_MVC.Dto_s;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.Services.Interfaces;
namespace TASK_MOCK_MVC.Controllers;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
public class TaskModelController : Controller
{
    private readonly ITaskRepository _taskRepository;
    private readonly UserManager<User> _userManager;
    private readonly IToastNotification _toastNotification;
    private readonly AppDbContext _appDbContext;
    public TaskModelController(ITaskRepository productRepository, UserManager<User> userManager, IToastNotification toastNotification, AppDbContext appDbContext)
    {
        _taskRepository = productRepository;
        _userManager = userManager;
        _toastNotification = toastNotification;
        _appDbContext = appDbContext;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();

        var productViewModels = tasks.Select(p => new TaskModel()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            DueDate = p.DueDate,
            Status = p.Status,
        }).ToList();

        return View(productViewModels);
    }
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult>Details(int id)
    {
        try
        {
            if (id == null) return NotFound();
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            _toastNotification.AddSuccessToastMessage("Detail Found");
            return View(task);
        }
        catch(Exception ex)
        {
            _toastNotification.AddErrorToastMessage("Detail not found");
            return RedirectToAction("Index", "Home");
        }
    }
    [Authorize(Roles = "ADMIN,MANAGER")]
    public IActionResult Create() => View();
    [Authorize(Roles = "ADMIN,MANAGER")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,Status,Email")] TaskModel task,string email)
    {
        if (!ModelState.IsValid)
        {
            return View(task);
        }
        task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);

        DateTime Limetdate = new DateTime(2050, 1, 1);
        var oldtime = DateTime.UtcNow;
        if(task.DueDate < Limetdate && task.DueDate < oldtime)
        {
            throw new Exception("Please Enter again date");
        }
        var user = await _userManager.GetUserAsync(HttpContext.User);
        await _taskRepository.CreateTaskAsync(task, email);
        await _taskRepository.CreateAudit(task, null, "Create", user);
        _toastNotification.AddSuccessToastMessage("Created successfully");
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "ADMIN")]
    public async Task <IActionResult>Edit(int id)
    {
        try
        {
            if (id == null) return NotFound();
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if(task == null) return NotFound();
            return View(task);
		}
        catch(Exception ex)
        {
            return RedirectToAction("Index", "Home"); 
        }
    }
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult>Edit(int id, [Bind("Id,Title,Description,DueDate,Status")] TaskModel task)
    {
        if (id != task.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            _toastNotification.AddErrorToastMessage("Bu yerda Items yoq.Please try again");
            return View(task);
        }
        try
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var oldTask = await _taskRepository.GetOldValueAsync(id);
            var newTask = await _taskRepository.UpdateTaskAsync(task);
            await _taskRepository.CreateAudit(newTask, oldTask,"Edit",user);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (_taskRepository.GetTaskByIdAsync(task.Id) == null) return NotFound();
            else
                throw;

        }
        _toastNotification.AddSuccessToastMessage("task change successfully");
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete() => View();
	[Authorize(Roles = "ADMIN")]
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
    public async Task <IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return View("Index");
        var task = await _taskRepository.DeleteTaskAsync(id);
        if (task == null) return NotFound();

        var user = await _userManager.GetUserAsync(HttpContext.User);
        await _taskRepository.CreateAudit(task, null, "Detele", user);
        _toastNotification.AddSuccessToastMessage("Your task deleted");
        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "Admin")]
    public IActionResult CreateTaskForUser()
    {
        return View();
    }
    [Authorize(Roles = "ADMIN")]
    public IActionResult EmailSender()
    {
        return View();
    }
    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    public async Task < IActionResult> EmailSender(NotificationModel model)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("abdumutalmahamatqulov6@gmail.com"));
        email.To.Add(MailboxAddress.Parse(model.Email));
        email.Subject = "Your verification code";
        email.Body = new TextPart(TextFormat.Html) { Text = "Your verification is " + model.Message };
        
        var smpt = new SmtpClient();
        await smpt.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smpt.AuthenticateAsync("abdumutalmahamatqulov6@gmail.com", "pmqepaowtziyhfei");
        await smpt.SendAsync(email);
        await smpt.DisconnectAsync(true);

        if(email is not null)
        {
            _toastNotification.AddSuccessToastMessage("Sender Successfully");
            return RedirectToAction("Index", "TaskModel");
        }
        return View();
    }
    public async Task<IActionResult> Index2()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();

        var productViewModels = tasks.Select(p => new TaskModel()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            DueDate = p.DueDate,
            Status = p.Status,
        }).ToList();

        return View(productViewModels);
    }
}
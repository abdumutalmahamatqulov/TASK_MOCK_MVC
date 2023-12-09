using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polly;
using Polly.Retry;
using TASK_MOCK_MVC.Dto_s;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.FluentValidation;
using TASK_MOCK_MVC.Services.Interfaces;
using TASK_MOCK_MVC.ViewModels;

namespace TASK_MOCK_MVC.Controllers;

public class AccountController : Controller
{
	private readonly IUserRepository _repository;
	private readonly IToastNotification _toastNotification;
	private readonly SignInManager<User> _signInManager;

	public AccountController(SignInManager<User> signInManager, IToastNotification toastNotification, IUserRepository repository)
	{
		_signInManager = signInManager;
		_toastNotification = toastNotification;
		_repository = repository;
	}
	public IActionResult Registor() => View();
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task <IActionResult> Registor(RegistorDto model)
	{

		try
		{
			if (ModelState.IsValid) await _repository.Register(model);
			_toastNotification.AddSuccessToastMessage("Registration successfully");
			return RedirectToAction("CreateUser", "Account");
		}
		catch(Exception ex)
		{
			_toastNotification.AddErrorToastMessage(ex.Message);
			return View(model);
		}

	}
    public IActionResult RegistorManager() => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegistorManager(RegistorDto model)
    {

        try
        {
            if (ModelState.IsValid) await _repository.Register(model);
            _toastNotification.AddSuccessToastMessage("Registration successfully");
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage(ex.Message);
            return View(model);
        }

    }
    public IActionResult Login() => View();
	[HttpPost]
	public async Task <IActionResult>Login(LoginDto model)
	 {

		var rePolicy = Policy.Handle<Exception>()
			.RetryAsync(3, (exception, retryCount) =>
			{
				Console.WriteLine($"exception occurred during  login. Retry attemp:{retryCount}.Exception: {exception}");
			});
		try
		{
			var signInResult = await rePolicy.ExecuteAsync(async () =>
			{
				var result = await _repository.Login(model);
				return result;
			});
			return RedirectToAction("Index", "TaskModel");
		}
		catch(Exception ex)
		{
			_toastNotification.AddErrorToastMessage(ex.Message);
			return View(model);
		}
	}
	public IActionResult AccessDenied() => RedirectToAction("Index", "Home");
	[HttpGet]
	public async Task <IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Main", "Account");
	}
	public async Task <IActionResult> Main()
	{
		return User.Identity!.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
	}
	public async Task <IActionResult > CreateUser(CreateUserModel models)
	{
		var validationResult = await new CreateModelValidator().ValidateAsync(models);

		try
		{
			await _repository.CreateUser(models);
			_toastNotification.AddSuccessToastMessage("Registration successfully");
			return RedirectToAction("Login", "Account");
		}
		catch (Exception ex)
		{
			_toastNotification.AddErrorToastMessage(ex.Message);
			return View(models);
		}
	}
}

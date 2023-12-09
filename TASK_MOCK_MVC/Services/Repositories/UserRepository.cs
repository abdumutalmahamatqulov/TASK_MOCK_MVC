using Microsoft.AspNetCore.Identity;
using TASK_MOCK_MVC.Data;
using TASK_MOCK_MVC.Dto_s;
using TASK_MOCK_MVC.Entities;
using TASK_MOCK_MVC.Entities.Enum;
using TASK_MOCK_MVC.ExensionFuntions;
using TASK_MOCK_MVC.Services.Interfaces;
using TASK_MOCK_MVC.ViewModels;

namespace TASK_MOCK_MVC.Services.Repositories;
public class UserRepository : IUserRepository
{
	private readonly AppDbContext _context;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public UserRepository(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext context)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_context = context;
	}

	public async Task<RegistorDto> Register(RegistorDto model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var exitUser = await _userManager.FindByEmailAsync(model.Email);
		if (exitUser != null)
			throw new Exception("Email already token");
		var user = new User { UserName = model.Name, Email = model.Email };
		var result =  await _userManager.CreateAsync(user, model.Password);
		if(result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "User");
			await _context.SaveChangesAsync();
		}
		foreach(var error in result.Errors)
			throw new Exception($"{error.Description}");
		return model ?? new RegistorDto();
	}
	public async Task<CreateUserModel> CreateUser(CreateUserModel model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var exitUser = await _userManager.FindByEmailAsync(model.Email);
		if (exitUser != null)
			throw new Exception("Email already token");
		var user = new User
		{
			UserName = model.Name,
			Email = model.Email,
		};
		var result = await _userManager.CreateAsync(user, model.Password);
		Console.WriteLine(result.Errors);
		if(model.Role==ERole.MANAGER)
		{
			if (!result.Succeeded) return model?? new CreateUserModel();
			await _userManager.AddToRoleAsync(user, "MANAGER");
			await _context.SaveChangesAsync();
		}
		else if(model.Role==ERole.USER)
		{
			if (!result.Succeeded) return model ?? new CreateUserModel();
			await _userManager.AddToRoleAsync(user, "USER");
			await _context.SaveChangesAsync();
		}
		else if (model.Role == ERole.ADMIN)
		{
			if (!result.Succeeded) return model ?? new CreateUserModel();
			await _userManager.AddToRoleAsync(user, "ADMIN");
			await _context.SaveChangesAsync();
		}
		return model ?? new CreateUserModel();
	}

	public async Task<SignInResult> Login(LoginDto model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var user  = await _userManager.FindByEmailAsync(model.Email);
		if (user == null) throw new Exception("Invalid email or password");
		var passResult = await _userManager.CheckPasswordAsync(user, model.Password);

		if (!passResult)
			throw new Exception("Invalid email or password");
		var result = await _signInManager.PasswordSignInAsync(user, model.Password,false,false);
		if (!result.Succeeded)
			throw new Exception("Invalid email or password");

		return result;
	}


	public async Task<RegistorDto> RegisterManager(RegistorDto model)
	{
		if (!CheckEmail.IsValidEmail(model.Email))
			throw new Exception("Invalid email address format");
		var exitUser = await _userManager.FindByEmailAsync(model.Email);
		if (exitUser != null)
			throw new Exception("Email already token");
		var user = new User 
		{
			UserName = model.Name,
			Email = model.Email,
		};
		var result = await _userManager.CreateAsync(user, model.Password);
		Console.WriteLine(result.Errors);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "MANAGER");
			await _context.SaveChangesAsync();
		}
		foreach (var error in result.Errors)
			throw new Exception($"{error.Description}");
		return model ?? new RegistorDto();
	}
}

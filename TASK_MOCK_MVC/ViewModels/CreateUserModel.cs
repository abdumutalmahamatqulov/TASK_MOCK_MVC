using System.ComponentModel.DataAnnotations;
using TASK_MOCK_MVC.Entities.Enum;

namespace TASK_MOCK_MVC.ViewModels;

public class CreateUserModel
{
	public string Name { get; set; }
	[EmailAddress(ErrorMessage = "Invalid Email Address")]
	public string Email { get; set; }
	public string Password { get; set; }
	public string ConfirmPassword { get; set; }
	public ERole Role { get; set; }
}

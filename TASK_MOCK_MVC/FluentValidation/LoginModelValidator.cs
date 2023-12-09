using FluentValidation;
using TASK_MOCK_MVC.Dto_s;
using TASK_MOCK_MVC.ExensionFuntions;

namespace TASK_MOCK_MVC.FluentValidation;
public class LoginModelValidator:AbstractValidator<LoginDto>
{
	public LoginModelValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email is required")
			.EmailAddress().WithMessage("Invalid email address");
		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Password is required")
			.MinimumLength(6).WithMessage("Password must be at least 6 character")
			.Must(CheckEmail.HaveCapitalLetter).WithMessage("Password must contain at least one capital letter");
	}
}

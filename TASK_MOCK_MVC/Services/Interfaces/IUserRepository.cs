using Microsoft.AspNetCore.Identity;
using TASK_MOCK_MVC.Dto_s;
using TASK_MOCK_MVC.ViewModels;

namespace TASK_MOCK_MVC.Services.Interfaces;
public interface IUserRepository
{

    Task<RegistorDto> Register(RegistorDto model);
 
    Task<RegistorDto> RegisterManager(RegistorDto model);
 
    Task<SignInResult> Login(LoginDto model);
	Task<CreateUserModel> CreateUser(CreateUserModel model);

}

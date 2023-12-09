using Microsoft.AspNetCore.Identity;

namespace TASK_MOCK_MVC.Entities;
public class User:IdentityUser
{
	public List<TaskModel> TaskModels { get; set; }
}

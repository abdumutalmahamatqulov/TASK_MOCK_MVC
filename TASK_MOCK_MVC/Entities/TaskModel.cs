using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TASK_MOCK_MVC.Entities;
public class TaskModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    public string Description { get; set; }
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set;}
    public EStatus Status { get; set; }
}

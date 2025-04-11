using Logic.ScheduleStuff;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebApp.Pages
{
    public class EmployeeSchedule_PageModel : PageModel
    {
        private readonly ScheduleManager _scheduleManager;

        public List<TaskModel> Tasks { get; private set; }

        public EmployeeSchedule_PageModel(ScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }

        public void OnGet()
        {
            var userName = User.FindFirstValue("Username");

            Tasks = GetTasksForUser(userName);
        }
        private List<TaskModel> GetTasksForUser(string userName)
        {
            var taskDTOs = _scheduleManager.GetTasksForUser(userName);
            return taskDTOs.Select(task => new TaskModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Date = task.StartDate,
                StartTime = task.StartDate,
                EndTime = task.EndDate,
                Location = task.Location.Name,
                IdModal = $"task{task.Id}Modal",
                IdModalLabel = $"task{task.Id}ModalLabel"
            }).ToList();
        }
    }
}

public class TaskModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; }
    public bool RepresentsShift { get; set; }
    public string IdModal { get; set; }
    public string IdModalLabel { get; set; }
}


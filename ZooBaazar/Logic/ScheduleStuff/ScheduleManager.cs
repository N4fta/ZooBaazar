using Data_Access;
using Data_Access.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ScheduleStuff
{
    public class ScheduleManager
    {
        private readonly ScheduleRepository scheduleRepository;
        private readonly LocationManager locationManager;
        private Schedule _schedule;

        public ScheduleManager(ScheduleRepository scheduleRepository, LocationManager locationManager)
        {
            this.scheduleRepository = scheduleRepository;
            this.locationManager = locationManager;
        }

        public void AddSchedule(Schedule schedule)
        {
            _schedule = schedule;
        }

        public Result SaveScheduleTasks()
        {
            List<TaskDTO> taskDTOs = new();
            foreach (var week in _schedule.TasksPerWeek)
            {
                foreach (var day in week.Value)
                {
                    taskDTOs.AddRange(ConvertToTaskDTO(day.Value));
                }
            }
            List<TaskDTO> shiftDTOs = new();
            foreach (var week in _schedule.ShiftsPerWeek)
            {
                foreach (var day in week.Value)
                {
                    shiftDTOs.AddRange(ConvertToTaskDTO(day.Value));
                }
            }
            Result result = scheduleRepository.InsertTasks(taskDTOs, shiftDTOs);
            return result;
        }

        public void UpdateTask(Task task)
        {
            // Update schedule task
            foreach (var week in _schedule.TasksPerWeek)
            {
                foreach (var day in week.Value)
                {
                    foreach (var scheduleTask in day.Value)
                    {
                        if (scheduleTask.Id == task.Id)
                        {
                            day.Value.Remove(scheduleTask);
                            day.Value.Add(task);
                            return;
                        }
                    }
                }
            }
        }

        public Schedule GetSchedule()
        {
            return _schedule;
        }

        public List<Task> GetTasksForUser(string username)
        {
            List<Task> results = null;
            try
            {
                results = ConvertToTask(scheduleRepository.GetTasksForUser(username));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        //public ScheduleDTO ConvertToScheduleDTO(Schedule schedule)
        //{
        //    var scheduleDTO = new ScheduleDTO();
        //    scheduleDTO.Id = schedule.ID;
        //    scheduleDTO.DateCreated = schedule.DateCreated;
        //    scheduleDTO.ValidUntil = schedule.ValidUntil;
        //    foreach (var week in schedule.TasksPerWeek)
        //    {
        //        scheduleDTO.TasksPerWeek.Add(week.Key, new());
        //        foreach (var day in week.Value)
        //        {
        //            var taskDTOs = ConvertToTaskDTO(day.Value);
        //            scheduleDTO.TasksPerWeek[week.Key].Add(day.Key,taskDTOs);
        //        }
        //    }
        //    foreach (var week in schedule.ShiftsPerWeek)
        //    {
        //        scheduleDTO.ShiftsPerWeek.Add(week.Key, new());
        //        foreach (var day in week.Value)
        //        {
        //            var shiftDTOs = ConvertToTaskDTO(day.Value);
        //            scheduleDTO.ShiftsPerWeek[week.Key].Add(day.Key, shiftDTOs);
        //        }
        //    }
        //    return scheduleDTO;
        //}
        //public Schedule ConvertToSchedule(ScheduleDTO scheduleDTO)
        //{
        //    var schedule = new Schedule(
        //        scheduleDTO.Id,
        //        scheduleDTO.DateCreated,
        //        scheduleDTO.ValidUntil);
            
        //    foreach (var week in scheduleDTO.TasksPerWeek)
        //    {
        //        schedule.TasksPerWeek.Add(week.Key, new());
        //        foreach (var day in week.Value)
        //        {
        //            var tasks = ConvertToTask(day.Value);
        //            schedule.TasksPerWeek[week.Key].Add(day.Key, tasks);
        //        }
        //    }

        //    foreach (var week in scheduleDTO.ShiftsPerWeek)
        //    {
        //        schedule.ShiftsPerWeek.Add(week.Key, new());
        //        foreach (var day in week.Value)
        //        {
        //            var shifts = ConvertToTask(day.Value);
        //            schedule.ShiftsPerWeek[week.Key].Add(day.Key, shifts);
        //        }
        //    }

        //    return schedule;
        //}

        #region ConvertToTaskDTOs
        private Task ConvertToTask(TaskDTO taskDTO)
        {
            // Needs to change
            Location? location = locationManager.Find(taskDTO.LocationID);

            //Converts string to enums
            WorkType workType = (WorkType)Enum.Parse(typeof(WorkType), taskDTO.WorkType);
            RepeatEnum repeat = (RepeatEnum)Enum.Parse(typeof(RepeatEnum), taskDTO.RepeatType);

            var task = new Task(
                    taskDTO.Id,
                    taskDTO.Name,
                    taskDTO.Description,
                    workType,
                    repeat,
                    taskDTO.NumberOfRepeats,
                    taskDTO.StartDate,
                    taskDTO.EndDate,
                    taskDTO.DayTask,
                    taskDTO.NightTask,
                    location
                );
            task.ScheduleTask = taskDTO.ScheduleTask;
            task.RepresentsShift = taskDTO.RepresentsShift;
            return task;
        }
        private List<Task> ConvertToTask(List<TaskDTO> taskDTOs)
        {
            List<Task> tasks = new();
            foreach (var taskDTO in taskDTOs)
            {
                tasks.Add(ConvertToTask(taskDTO));
            }
            return tasks;
        }

        private TaskDTO ConvertToTaskDTO(Task task)
        {
            int locationID = 0;
            if (task.Location != null) locationID = task.Location.Id;
            else if (task.RepresentsShift) locationID = 1009; // Represents zoo

            // This converts a Task to TaskDTO
            var taskDTO = new TaskDTO();
            taskDTO.Id = task.Id;
            taskDTO.Name = task.Name;
            taskDTO.Description = task.Description;
            taskDTO.StartDate = task.StartDate;
            taskDTO.EndDate = task.EndDate;
            taskDTO.DayTask = task.DayTask;
            taskDTO.NightTask = task.NightTask;
            taskDTO.RepeatType = task.RepeatType.ToString();
            taskDTO.NumberOfRepeats = task.NumberOfRepeats;
            taskDTO.WorkType = task.WorkType.ToString();
            taskDTO.LocationID = locationID;
            taskDTO.EmployeeID = task.Employees.First().EmployeeID;
            taskDTO.ScheduleTask = task.ScheduleTask;
            taskDTO.RepresentsShift = task.RepresentsShift;

            return taskDTO;
        }
        private List<TaskDTO> ConvertToTaskDTO(List<Task> tasks)
        {
            List<TaskDTO> taskDTOs = new();
            foreach (var task in tasks)
            {
                taskDTOs.Add(ConvertToTaskDTO(task));
            }
            return taskDTOs;
        }
        #endregion
    }
}

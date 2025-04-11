using Data_Access;
using System.Threading.Tasks;


namespace Logic.ScheduleStuff
{
    public class TaskManager
    {
        private readonly ITaskRepository taskRepository;
        private readonly LocationManager locationManager;

        public TaskManager(ITaskRepository taskRepository, LocationManager locationManager)
        {
            this.taskRepository = taskRepository;
            this.locationManager = locationManager;
        }

        public Result Add(Task task)
        {
            Result result = taskRepository.InsertTask(ConvertToTaskDTO(task));
            return result;
        }

        public List<Task> LoadTaskFromDataBase()
        {
            List<Task> results = new List<Task>();
            try
            {
                 results = ConvertToTask(taskRepository.LoadTasks());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        public Result Update(Task task)
        {
            Result result = taskRepository.UpdateTask(ConvertToTaskDTO(task));
            return result;
        }

        public Result Remove(Task task)
        {
            Result result = taskRepository.DeleteTask(ConvertToTaskDTO(task));
            return result;
        }

        public Task ConvertToTask(TaskDTO taskDTO)
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
            return task;
        }
        public List<Task> ConvertToTask(List<TaskDTO> taskDTOs)
        {
            List<Task> tasks = new();
            foreach (var taskDTO in taskDTOs)
            {
                tasks.Add(ConvertToTask(taskDTO));
            }
            return tasks;
        }

        public TaskDTO ConvertToTaskDTO(Task task)
        {
            int locationID = 0;
            if (task.Location != null) locationID = task.Location.Id;

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
            taskDTO.ScheduleTask = task.ScheduleTask;

            return taskDTO;
        }
        public List<TaskDTO> ConvertToTaskDTO(List<Task> tasks)
        {
            List<TaskDTO> taskDTOs = new();
            foreach (var task in tasks)
            {
                taskDTOs.Add(ConvertToTaskDTO(task));
            }
            return taskDTOs;
        }
    }
}

using Logic.Interfaces;

namespace Logic.ScheduleStuff
{
    public class TaskMakerLocations : ITaskMaker
    {
        private readonly List<Location> locations;

        public TaskMakerLocations(List<Location> locations)
        {
            this.locations = locations;
        }

        public List<Task> GenerateTasks()
        {
            List<Task> locationTasks = new();
            DateTime dateTime = Enumerable.Range(0, 6).Select(i => DateTime.Now.AddDays(i)).Single(day => day.DayOfWeek == DayOfWeek.Monday); // Creates an Enumerable with all Days of the week and selects Monday
            dateTime = dateTime.AddHours(-dateTime.Hour); // Sets Hour to 0
            dateTime = dateTime.AddMinutes(-dateTime.Minute); // Sets Minutes to 0
            dateTime = dateTime.AddSeconds(-dateTime.Second); // Sets Seconds to 0
            // Add a feeding and cleaning daily task
            foreach (Location location in locations)
            {
                // var multiplier = location.AnimalCount / 5;
                locationTasks.Add(new Task(
                    location.Name + " Feeding",
                    $"Auto-generated task for feeding {location.Name}",
                    WorkType.Caretaker,
                    RepeatEnum.Daily,
                    3,
                    dateTime,
                    dateTime.AddHours(2),
                    true,
                    false,
                    location
                    ));
                locationTasks.Add(new Task(
                    location.Name + " Cleaning",
                    $"Auto-generated task for cleaning {location.Name}",
                    WorkType.Cleaner,
                    RepeatEnum.Daily,
                    1,
                    dateTime,
                    dateTime.AddHours(2),
                    false,
                    true,
                    location
                    ));
            }
            return locationTasks;
        }
    }
}

namespace Logic.ScheduleStuff
{

    public class Task
    {
        public int Id { get; set; }

        public string Name { get; }

        public List<Employee> Employees { get; } = new();

        public string Description { get; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public RepeatEnum RepeatType { get; }

        public int NumberOfRepeats  { get; }

        public WorkType WorkType { get; }

        public bool NightTask { get; }

        public bool DayTask { get; }

        public Location Location { get; }

        // Hidden properties (not in constructor), only assigned when needed
        public bool ScheduleTask { get; set; } = false;

        public bool RepresentsShift { get; set; } = false;


        public Task(int id, string name, string description, WorkType workType, RepeatEnum repeatType, int numberOfRepeats, DateTime startDate, DateTime endDate, bool dayTask, bool nightTask, Location location)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            DayTask = dayTask;
            NightTask = nightTask;
            RepeatType = repeatType;
            NumberOfRepeats = numberOfRepeats;
            WorkType = workType;
            Location = location;
        }

        public Task(string name, string description, WorkType workType, RepeatEnum repeatType, int numberOfRepeats, DateTime startDate, DateTime endDate, bool dayTask, bool nightTask, Location location)
        {
            Id = 0;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            DayTask = dayTask;
            NightTask = nightTask;
            RepeatType = repeatType;
            NumberOfRepeats = numberOfRepeats;
            WorkType = workType;
            Location = location;
        }

        public Task ShallowClone()
        {
            var task = new Task(
                this.Id++,
                this.Name,
                this.Description,
                this.WorkType,
                this.RepeatType,
                this.NumberOfRepeats,
                this.StartDate,
                this.EndDate,
                this.DayTask,
                this.NightTask,
                this.Location
                );
            task.ScheduleTask = ScheduleTask;
            return task;
        }
        public Task ShallowCloneWithExtraDays(int days)
        {
            var task = new Task(
                this.Id++,
                this.Name,
                this.Description,
                this.WorkType,
                this.RepeatType,
                this.NumberOfRepeats,
                this.StartDate.AddDays(days),
                this.EndDate.AddDays(days),
                this.DayTask,
                this.NightTask,
                this.Location
                );
            task.ScheduleTask = ScheduleTask;
            return task;
        }

        public override string ToString()
        {
            return $"{Name} - {StartDate.ToString("ddd HH")}h to {EndDate.ToString("HH")}h";
        }

    }
}

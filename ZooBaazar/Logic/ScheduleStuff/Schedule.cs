namespace Logic.ScheduleStuff
{
    public class Schedule
    {
        public int ID { get; } = 0;
        public DateTime DateCreated;
        public DateTime ValidUntil;
        // A Dictionary with weeks as values
        // The DateTime represents the Monday that weeks starts on
        // Example: List<Task> tuesdayTasks = Weeks[DateTime.Now.AddDays(7)][DayOfWeek.Tuesday];
        public Dictionary<DateTime, Dictionary<DayOfWeek, List<Task>>> TasksPerWeek = new();

        // Same but for Employee Shifts
        public Dictionary<DateTime, Dictionary<DayOfWeek, List<Task>>> ShiftsPerWeek = new();


        public Schedule(DateTime validUntil, Dictionary<DateTime, Dictionary<DayOfWeek, List<Task>>>? tasksPerWeek = null, Dictionary<DateTime, Dictionary<DayOfWeek, List<Task>>>? shiftsPerWeek = null)
        {
            DateCreated = DateTime.Now;
            ValidUntil = validUntil;
            if (tasksPerWeek != null) TasksPerWeek = tasksPerWeek;
            if (shiftsPerWeek != null) ShiftsPerWeek = shiftsPerWeek;
        }
        public Schedule(int ID, DateTime DateCreated, DateTime validUntil, Dictionary<DateTime, Dictionary<DayOfWeek, List<Task>>>? tasksPerWeek = null, Dictionary<DateTime, Dictionary<DayOfWeek, List<Task>>>? shiftsPerWeek = null)
        {
            ID = ID;
            DateCreated = DateCreated;
            ValidUntil = validUntil;
            if (tasksPerWeek != null) TasksPerWeek = tasksPerWeek;
            if (shiftsPerWeek != null) ShiftsPerWeek = shiftsPerWeek;
        }

    }
}

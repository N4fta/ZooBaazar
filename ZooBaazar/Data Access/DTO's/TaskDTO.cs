namespace Data_Access
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RepeatType { get; set; }
        public int NumberOfRepeats { get; set; }
        public string WorkType { get; set; }
        public bool NightTask { get; set; }
        public bool DayTask { get; set; }
        public int LocationID { get; set; }
        public int EmployeeID { get; set; }
        public bool ScheduleTask { get; set; } = false;
        public bool RepresentsShift { get; set; } = false;
    }
}

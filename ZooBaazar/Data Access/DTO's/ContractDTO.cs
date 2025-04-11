namespace Data_Access
{
    public class ContractDTO
    {
        public int ContractID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime startDate {  get; set; }
        public DateTime endDate { get; set; }
        public double salary { get; set; }
        public string role { get; set; }
        public int bsn { get; set; }
        public int bankNumber { get; set; }
        public int hoursPerWeek { get; set; }
        public bool changeShifts { get; set; }
        public bool overtime { get; set; }
        public bool dayShifts { get; set; }
        public bool nightShifts { get; set; }
        public List<DayOfWeek> workDays { get; set; }
        public int paidLeaveDays { get; set; }
        public int unpaidLeaveDays { get; set; }
        public string Notes { get; set; }
        public string contractType { get; set; }
    }
}

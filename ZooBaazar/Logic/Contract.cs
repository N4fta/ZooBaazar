namespace Logic
{
    public class Contract
    {
        public int ContractID;
        public int EmployeeID;
        public DateTime startDate;
        public DateTime endDate;
        public double salary;
        public WorkType role;
        public int bsn;
        public int bankNumber;
        public int hoursPerWeek;
        public bool changeShifts;
        public bool overtime;
        public bool dayShifts;
        public bool nightShifts;
        public List<DayOfWeek> workDays;
        public int paidLeaveDays;
        public int unpaidLeaveDays;
        public string Notes { get; set; }
        public ContractType contractType;

        public Contract(int contractID, DateTime startDate, DateTime endDate, double salary, WorkType role, int bsn, int bankNumber, int hoursPerWeek, bool changeShifts, bool overtime, bool dayShifts, bool nightShifts, List<DayOfWeek> workDays, int paidLeaveDays, int unpaidLeaveDays, string notes, ContractType contractType)
        {
            ContractID = contractID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.salary = salary;
            this.role = role;
            this.bsn = bsn;
            this.bankNumber = bankNumber;
            this.hoursPerWeek = hoursPerWeek;
            this.changeShifts = changeShifts;
            this.overtime = overtime;
            this.dayShifts = dayShifts;
            this.nightShifts = nightShifts;
            this.workDays = workDays;
            this.paidLeaveDays = paidLeaveDays;
            this.unpaidLeaveDays = unpaidLeaveDays;
            Notes = notes;
            this.contractType = contractType;
        }

        public Contract(DateTime startDate, DateTime endDate, double salary, WorkType role, int bsn, int bankNumber, int hoursPerWeek, bool changeShifts, bool overtime, bool dayShifts, bool nightShifts, List<DayOfWeek> workDays, int paidLeaveDays, int unpaidLeaveDays, string notes, ContractType contractType)
        {
            ContractID = 0;
            this.startDate = startDate;
            this.endDate = endDate;
            this.salary = salary;
            this.role = role;
            this.bsn = bsn;
            this.bankNumber = bankNumber;
            this.hoursPerWeek = hoursPerWeek;
            this.changeShifts = changeShifts;
            this.overtime = overtime;
            this.dayShifts = dayShifts;
            this.nightShifts = nightShifts;
            this.workDays = workDays;
            this.paidLeaveDays = paidLeaveDays;
            this.unpaidLeaveDays = unpaidLeaveDays;
            Notes = notes;
            this.contractType = contractType;
        }

        public Contract(int contractID, int employeeID, DateTime startDate, DateTime endDate, double salary, WorkType role, int bsn, int bankNumber, int hoursPerWeek, bool changeShifts, bool overtime, bool dayShifts, bool nightShifts, List<DayOfWeek> workDays, int paidLeaveDays, int unpaidLeaveDays, string notes, ContractType contractType)
        {
            ContractID = contractID;
            EmployeeID = employeeID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.salary = salary;
            this.role = role;
            this.bsn = bsn;
            this.bankNumber = bankNumber;
            this.hoursPerWeek = hoursPerWeek;
            this.changeShifts = changeShifts;
            this.overtime = overtime;
            this.dayShifts = dayShifts;
            this.nightShifts = nightShifts;
            this.workDays = workDays;
            this.paidLeaveDays = paidLeaveDays;
            this.unpaidLeaveDays = unpaidLeaveDays;
            Notes = notes;
            this.contractType = contractType;
        }
    }
}

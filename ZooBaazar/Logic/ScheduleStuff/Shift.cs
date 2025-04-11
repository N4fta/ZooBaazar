using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ScheduleStuff
{
    // Class only exists while creating schedule
    public class Shift
    {
        public int Id { get; set;  }

        public Employee Employee { get; set; }

        // only for schedule
        public List<Task> Tasks { get; } = new List<Task>();

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool DayShift { get; set; }

        public bool NightShift { get; set; }

        public Shift(int id, Employee employee, DateTime? startDate, DateTime? endDate, bool dayShift, bool nightShift)
        {
            Id = id;
            Employee = employee;
            StartDate = startDate;
            EndDate = endDate;
            DayShift = dayShift;
            NightShift = nightShift;
        }
        public Shift(Employee employee, DateTime? startDate, DateTime? endDate, bool dayShift, bool nightShift)
        {
            Id = 0;
            Employee = employee;
            StartDate = startDate;
            EndDate = endDate;
            DayShift = dayShift;
            NightShift = nightShift;
        }

        public Task ConvertToTask()
        {
            var task = new Task(
                10000+(Id++),
                $"Shift {Employee.Name}",
                $"Automatically created shift for  {Employee.Name}",
                Employee.Role,
                RepeatEnum.Never,
                1,
                StartDate.Value,
                EndDate.Value,
                DayShift,
                NightShift,
                null
                );
            task.Employees.Add(Employee);
            task.RepresentsShift = true;
            return task;
        }

        public Shift ShallowClone()
        {
            return new Shift(
                this.Id++,
                this.Employee,
                this.StartDate,
                this.EndDate,
                this.DayShift,
                this.NightShift
                );
        }
    }
}

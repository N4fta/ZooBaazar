using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ScheduleStuff.Makers
{
    public class ShiftMakerDefault : IShiftMaker
    {
        private readonly List<Employee> employees;
        public readonly int shiftLenghtInHours;

        public ShiftMakerDefault(List<Employee> employees, int shiftLenghtInHours)
        {
            this.employees = employees;
            this.shiftLenghtInHours = shiftLenghtInHours;
        }

        public List<KeyValuePair<int, Shift>> GenerateShifts()
        {
            // Amount of Shifts and shift
            List<KeyValuePair<int, Shift>> employeeShifts = new();
            foreach (Employee employee in employees)
            {
                if (employee.Contract == null) continue;

                int hoursPerWeek = employee.Contract.hoursPerWeek;
                int numberOfShifts = 0;
                DateTime dateTime = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(i)).Single(day => day.DayOfWeek == DayOfWeek.Monday); // Creates an Enumerable with all Days of the week and selects Monday
                // If employee has overtime we round the number of shifts up
                if (employee.Contract.overtime)
                {
                    numberOfShifts = (int)Math.Ceiling((double)hoursPerWeek / shiftLenghtInHours);
                }
                // else down
                else
                {
                    numberOfShifts = (int)Math.Floor((double)hoursPerWeek / shiftLenghtInHours);
                }
                employeeShifts.Add(new(numberOfShifts,
                    new(employee,
                    dateTime,
                    dateTime.AddHours(shiftLenghtInHours),
                    employee.Contract.dayShifts,
                    employee.Contract.nightShifts)));
            }
            return employeeShifts;
        }
    }
}

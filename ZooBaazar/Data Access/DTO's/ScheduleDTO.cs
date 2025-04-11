using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DTO_s
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ValidUntil { get; set; }
        public Dictionary<DateTime, Dictionary<DayOfWeek, List<TaskDTO>>> TasksPerWeek { get; } = new();
        public Dictionary<DateTime, Dictionary<DayOfWeek, List<TaskDTO>>> ShiftsPerWeek { get; } = new();

    }
}

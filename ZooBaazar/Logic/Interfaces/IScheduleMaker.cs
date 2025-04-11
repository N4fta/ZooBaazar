using Logic.ScheduleStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IScheduleMaker
    {
        public Schedule GenerateSchedule(DateTime validUntil);
    }
}

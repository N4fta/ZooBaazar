using Logic.ScheduleStuff;

namespace Logic.Interfaces
{
    public interface IShiftMaker
    {
        List<KeyValuePair<int, Shift>> GenerateShifts();
    }
}

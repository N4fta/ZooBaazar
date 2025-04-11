namespace Logic
{
    public class DayRecords
    {
            private List<DayRecord> dayRecords;

            public DayRecords()
            {
            dayRecords = new List<DayRecord>();
            }

            public void Add(DayRecord record)
            {
                dayRecords.Add(record);
            }

            public DayRecord[] GetArray()
            {
                return dayRecords.ToArray();
            }

            public bool Remove(DayRecord record)
            {
                return dayRecords.Remove(record);
            }
    }
}

namespace Logic
{
    public class DayRecord
    {
        public int numberOfVisitors;
        public int profits;
        public DateTime date;

        public DayRecord()
        {
            numberOfVisitors = 0;
            profits = 0;
            date = DateTime.Today;
        }
        public DayRecord(DateTime time, int visitors = 0, int profits = 0)
        {
            this.numberOfVisitors = visitors;
            this.profits = profits;
            this.date = time;
        }
    }
}

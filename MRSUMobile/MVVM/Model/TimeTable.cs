namespace MRSUMobile.MVVM.Model
{
    public class TimeTable
    {
        public DateTimeOffset Date { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
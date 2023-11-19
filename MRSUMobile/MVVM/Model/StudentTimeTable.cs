namespace MRSUMobile.MVVM.Model
{
    public class StudentTimeTable
    {
        public string Group { get; set; }

        public string PlanNumber { get; set; }

        public string FacultyName { get; set; }

        public int TimeTableBlockd { get; set; }

        public TimeTable TimeTable { get; set; }
    }
}
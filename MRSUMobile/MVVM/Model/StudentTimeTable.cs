namespace MRSUMobile.MVVM.Model
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public int CampusId { get; set; }
        public string CampusTitle { get; set; }
    }

    public class Discipline
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int LessonType { get; set; }
        public bool Remote { get; set; }
        public string Group { get; set; }
        public int SubgroupNumber { get; set; }
        public Teacher Teacher { get; set; }
        public Auditorium Auditorium { get; set; }
    }

    public class Lesson
    {
        public int Number { get; set; }
        public int SubgroupCount { get; set; }
        public List<Discipline> Disciplines { get; set; }
    }

    public class StudentTimeTable
    {
        public string Group { get; set; }
        public string PlanNumber { get; set; }
        public string FacultyName { get; set; }
        public int TimeTableBlockd { get; set; }
        public TimeTable TimeTable { get; set; }
    }

    public class Teacher
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FIO { get; set; }
        public Photo Photo { get; set; }
    }

    public class TimeTable
    {
        public DateTime Date { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
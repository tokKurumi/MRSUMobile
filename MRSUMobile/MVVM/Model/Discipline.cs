namespace MRSUMobile.MVVM.Model
{
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
}
namespace MRSUMobile.MVVM.Model
{
    public class Lesson
    {
        public byte Number { get; set; }

        public byte SubgroupCount { get; set; }

        public List<Discipline> Disciplines { get; set; }
    }
}
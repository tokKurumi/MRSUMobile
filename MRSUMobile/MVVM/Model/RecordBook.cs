namespace MRSUMobile.MVVM.Model
{
    public class RecordBook
    {
        public string Cod { get; set; }

        public string Number { get; set; }

        public string Faculty { get; set; }

        public List<Discipline> Disciplines { get; set; }
    }
}
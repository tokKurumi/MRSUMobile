namespace MRSUMobile.MVVM.Model
{
    public class StudentSemestr
    {
        public List<RecordBook> RecordBooks { get; set; }

        public int UnreadedDisCount { get; set; }

        public int UnreadedDisMesCount { get; set; }

        public string Year { get; set; }

        public int Period { get; set; }
    }
}
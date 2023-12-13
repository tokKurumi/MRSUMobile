namespace MRSUMobile.MVVM.Model
{
    public class DocFile
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public string MIMEtype { get; set; }

        public int Size { get; set; }

        public DateTime Date { get; set; }

        public string URL { get; set; }
    }
}
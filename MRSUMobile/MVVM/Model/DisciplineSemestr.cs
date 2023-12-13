namespace MRSUMobile.MVVM.Model
{
    public class DisciplineSemestr
    {
        public bool Relevance { get; set; }

        public bool IsTeacher { get; set; }

        public int UnreadedCount { get; set; }

        public int UnreadedMessageCount { get; set; }

        public List<string> Groups { get; set; }

        public List<DocFile> DocFiles { get; set; }

        public WorkingProgramm WorkingProgramm { get; set; }

        public int Id { get; set; }

        public string PlanNumber { get; set; }

        public string Year { get; set; }

        public string Faculty { get; set; }

        public string EducationForm { get; set; }

        public string EducationLevel { get; set; }

        public string Specialty { get; set; }

        public string SpecialtyCod { get; set; }

        public string Profile { get; set; }

        public string PeriodString { get; set; }

        public int PeriodInt { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }
    }
}
namespace MRSUMobile.MVVM.Model;

public class Report
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DocFile DocFile { get; set; }
}
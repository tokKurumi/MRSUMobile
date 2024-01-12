namespace MRSUMobile.MVVM.Model;

public class ControlDot
{
    public Mark Mark { get; set; }

    public Report Report { get; set; }

    public int Id { get; set; }

    public int Order { get; set; }

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public double MaxBall { get; set; }

    public bool IsReport { get; set; }

    public bool IsCredit { get; set; }

    public string CreatorId { get; set; }

    public DateTime CreateDate { get; set; }

    public List<TestProfile> TestProfiles { get; set; }
}
namespace MRSUMobile.MVVM.Model;

public class Section
{
    public List<ControlDot> ControlDots { get; set; }

    public int SectionType { get; set; }

    public int Id { get; set; }

    public int Order { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string CreatorId { get; set; }

    public DateTime CreateDate { get; set; }
}
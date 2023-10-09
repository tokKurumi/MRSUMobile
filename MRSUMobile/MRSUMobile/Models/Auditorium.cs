namespace MRSUMobile.Models
{
	public class Auditorium
	{
		public int Id { get; set; }
		public string Number { get; set; }
		public string Title { get; set; }
		public int CampusId { get; set; }
		public string CampusTitle { get; set; }
	}
}
namespace MRSUMobile.MVVM.Model
{
	public class StudentAttendanceCode
	{
		public int DisciplineId { get; set; }
		public string DisciplineTitle { get; set; }
		public DateTime Date { get; set; }
		public Teacher Teacher { get; set; }
	}
}
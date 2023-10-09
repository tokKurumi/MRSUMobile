using System;

namespace MRSUMobile.Models
{
	public class StudentAttendance
	{
		public int DisciplineId { get; set; }
		public string DisciplineTitle { get; set; }
		public DateTime Date { get; set; }
		public Teacher Teacher { get; set; }
	}
}
using System;
using System.Collections.Generic;

namespace MRSUMobile.Models
{
	public class Lesson
	{
		public int Number { get; set; }
		public int SubgroupCount { get; set; }
		public List<Discipline> Disciplines { get; set; }
	}

	public class Diary
	{
		public string Group { get; set; }
		public string PlanNumber { get; set; }
		public string FacultyName { get; set; }
		public int TimeTableBlockd { get; set; }
		public TimeTable TimeTable { get; set; }
	}

	public class TimeTable
	{
		public DateTime Date { get; set; }
		public List<Lesson> Lessons { get; set; }
	}
}
using System.Collections.Generic;
using System;

namespace MRSUMobile.Models.User
{
	public class Role
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}

	public class User
	{
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string EnglishFIO { get; set; }
		public string TeacherCod { get; set; }
		public string StudentCod { get; set; }
		public DateTime BirthDate { get; set; }
		public string AcademicDegree { get; set; }
		public string AcademicRank { get; set; }
		public List<Role> Roles { get; set; }
		public string Id { get; set; }
		public string UserName { get; set; }
		public string FIO { get; set; }
		public Photo Photo { get; set; }
	}
}
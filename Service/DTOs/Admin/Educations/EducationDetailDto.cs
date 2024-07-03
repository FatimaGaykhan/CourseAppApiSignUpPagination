using System;
namespace Service.DTOs.Admin.Educations
{
	public class EducationDetailDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string CreateDate { get; set; }

		public List<string> Groups { get; set; }

	}
}


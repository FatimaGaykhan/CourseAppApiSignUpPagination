using System;
namespace Service.DTOs.Admin.Groups
{
	public class GroupDetailDto
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatedDate { get; set; }

        public int Capacity { get; set; }

        public IEnumerable<string> Rooms { get; set; }

        public IEnumerable<string> Educations { get; set; }




        public IEnumerable<string> GroupStudents { get; set; }

    }
}


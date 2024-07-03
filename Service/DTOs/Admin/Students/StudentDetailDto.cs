using System;
namespace Service.DTOs.Admin.Students
{
	public class StudentDetailDto
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string CreatedDate { get; set; }

        public List<string> StudentGroups { get; set; }
    }
}


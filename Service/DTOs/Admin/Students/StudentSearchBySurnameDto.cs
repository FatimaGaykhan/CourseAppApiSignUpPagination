using System;
namespace Service.DTOs.Admin.Students
{
	public class StudentSearchBySurnameDto
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
    }
}


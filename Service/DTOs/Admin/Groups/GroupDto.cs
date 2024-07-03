using System;
namespace Service.DTOs.Admin.Groups
{
	public class GroupDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string CreatedDate { get; set; }

        public int Capacity { get; set; }

    }
}


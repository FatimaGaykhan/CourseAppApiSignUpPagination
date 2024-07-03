using System;
using Domain.Common;

namespace Domain.Entities
{
	public class GroupTeacher:BaseEntity
	{
		public Group Group { get; set; }

		public int GroupId { get; set; }

		public Teacher Teacher { get; set; }

		public int TeacherId { get; set; }

	}
}


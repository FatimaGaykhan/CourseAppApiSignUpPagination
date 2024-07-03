using System;
using Domain.Common;

namespace Domain.Entities
{
	public class GroupStudent:BaseEntity
	{

		public Group Group { get; set; }

		public int GroupId { get; set; }

		public Student Student { get; set; }

		public int StudentId { get; set; }

	}
}


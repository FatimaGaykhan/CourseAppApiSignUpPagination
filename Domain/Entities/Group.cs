using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Domain.Common;

namespace Domain.Entities
{
	public class Group:BaseEntity
	{
		public string Name { get; set; }

		public int Capacity { get; set; }

		public Education Education { get; set; }

		public int EducationId { get; set; }

		public Room Room { get; set; }

		public int RoomId { get; set; }

		public ICollection<GroupStudent> GroupStudents{ get; set; }

	}
}


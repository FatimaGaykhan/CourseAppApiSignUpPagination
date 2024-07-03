using System;
using Domain.Common;

namespace Domain.Entities
{
	public class Education:BaseEntity
	{
		public string Name { get; set; }
		public List<Group> Groups { get; set; }

	}
}


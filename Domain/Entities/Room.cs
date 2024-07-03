using System;
using Domain.Common;

namespace Domain.Entities
{
	public class Room:BaseEntity
	{
		public string Name { get; set; }

		public int SeatCount { get; set; }

		public List<Group> Groups { get; set; }

	}
}


using System;
namespace Service.DTOs.Admin.Rooms
{
	public class RoomDetailDto
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public int SeatCount { get; set; }

        public string CreateDate { get; set; }

        public List<string> Groups { get; set; }

    }
}


using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<Room>> SortBy(string text, bool isDescending = false)
        {
            var rooms = _context.Rooms.AsQueryable();

            switch (text.Trim().ToLower())
            {
                case "seatcount":
                    rooms = isDescending == true ? rooms.OrderByDescending(g => g.SeatCount) : rooms.OrderBy(g => g.SeatCount);
                    break;
                case "name":
                    rooms = isDescending == true ? rooms.OrderByDescending(g => g.Name) : rooms.OrderBy(g => g.Name);
                    break;
                default:
                    return await rooms.ToListAsync();
            }

            return await rooms.ToListAsync();
        }
    }
}


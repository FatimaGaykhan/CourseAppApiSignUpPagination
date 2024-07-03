using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers;
using Repository.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Repositories
{
	public class GroupRepository:BaseRepository<Group>, IGroupRepository
	{
		public GroupRepository(AppDbContext context) : base(context)
        {
			
		}

        public async Task<IEnumerable<Group>> GetPaginateDatasAsync(int page, int take)
        {
            return await _entities.Skip((page - 1) * take).Take(take).ToListAsync();
        }

        public async Task<List<Group>> SearchByName(string searchName)
        {
            return await _context.Groups.Where(m => m.Name.Contains(searchName)).ToListAsync();
        }

        public async Task<List<Group>> SortBy(string text, bool isDescending = false)
        {
            var groups = _context.Groups.AsQueryable();

            switch (text.Trim().ToLower())
            {
                case "capacity":
                    groups = isDescending == true ? groups.OrderByDescending(g => g.Capacity) : groups.OrderBy(g => g.Capacity);
                    break;
                case "name":
                    groups = isDescending == true ? groups.OrderByDescending(g => g.Name) : groups.OrderBy(g => g.Name);
                    break;
                default:
                    return await groups.ToListAsync();
            }

            return await groups.ToListAsync();

        }
    }
}


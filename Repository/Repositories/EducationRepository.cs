using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        public EducationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Education>> SortBy(string text, bool isDescending = false)
        {
            var educations = _context.Educations.AsQueryable();

            switch (text.Trim().ToLower())
            {
                case "name":
                    educations = isDescending == true ? educations.OrderByDescending(g => g.Name) : educations.OrderBy(g => g.Name);
                    break;
                default:
                    return await educations.ToListAsync();
            }

            return await educations.ToListAsync();
        }
    }
}


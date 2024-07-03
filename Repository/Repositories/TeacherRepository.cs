using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Teacher>> SortBy(string text, bool isDescending = false)
        {
            var teachers = _context.Teachers.AsQueryable();

            switch (text.Trim().ToLower())
            {
                case "salary":
                    teachers = isDescending == true ? teachers.OrderByDescending(g => g.Salary) : teachers.OrderBy(g => g.Salary);
                    break;
                case "name":
                    teachers = isDescending == true ? teachers.OrderByDescending(g => g.Name) : teachers.OrderBy(g => g.Name);
                    break;
                case "age":
                    teachers = isDescending == true ? teachers.OrderByDescending(g => g.Age) : teachers.OrderBy(g => g.Age);
                    break;
                default:
                    return await teachers.ToListAsync();
            }

            return await teachers.ToListAsync();
        }
    }
}


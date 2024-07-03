using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class StudentRepository:BaseRepository<Student>,IStudentRepository
	{
		public StudentRepository(AppDbContext context):base(context)
		{

		}

        public async Task AddToGroup(GroupStudent groupStudent)
        {
            await _context.GroupStudents.AddAsync(groupStudent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroup(GroupStudent groupStudent)
        {

            _context.GroupStudents.Remove(groupStudent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupStudent>> GetAllGroupStudents()
        {
            return await _context.GroupStudents.ToListAsync();
        }

        public async Task<List<Student>> SearchByNameOrSurname(string searchText)
        {
            return await _context.Students.Where(m => m.Name.Contains(searchText)|| m.Surname.Contains(searchText)).ToListAsync();
        }

     
    }
}


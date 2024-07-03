using System;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
	public interface IStudentRepository:IBaseRepository<Student>
	{
        Task DeleteGroup(GroupStudent groupStudent);
        Task AddToGroup(GroupStudent groupStudent);

        Task<List<Student>> SearchByNameOrSurname(string searchText);
        Task<IEnumerable<GroupStudent>> GetAllGroupStudents();
    }
}


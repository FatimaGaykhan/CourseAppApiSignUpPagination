using System;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
	public interface ITeacherRepository:IBaseRepository<Teacher>
	{
        Task<List<Teacher>> SortBy(string text, bool isDescending = false);

    }
}


using System;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
	public interface IEducationRepository:IBaseRepository<Education>
	{
        Task<List<Education>> SortBy(string text, bool isDescending = false);
    }
}


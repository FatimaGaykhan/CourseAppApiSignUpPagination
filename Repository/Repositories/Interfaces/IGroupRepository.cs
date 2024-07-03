using System;
using Domain.Entities;
using Repository.Helpers;

namespace Repository.Repositories.Interfaces
{
	public interface IGroupRepository:IBaseRepository<Group>
    {
		Task<List<Group>> SearchByName(string searchName);

		Task<List<Group>> SortBy(string text,bool isDescending=false);

		Task<IEnumerable<Group>> GetPaginateDatasAsync(int page,int take);
	}
}


using System;
using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository.Repositories.Interfaces
{
	public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<List<Room>> SortBy(string text, bool isDescending = false);
    }
}


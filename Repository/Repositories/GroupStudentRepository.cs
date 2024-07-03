using System;
using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class GroupStudentRepository:BaseRepository<GroupStudent>, IGroupStudentRepository
	{
		public GroupStudentRepository( AppDbContext context):base(context)
		{

		}
	}
}


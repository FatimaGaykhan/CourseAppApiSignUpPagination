using System;
using Repository.Helpers;
using Service.DTOs.Admin.Groups;

namespace Service.Services.Interfaces
{
	public interface IGroupService
	{
        Task CreateAsync(GroupCreateDto model);
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int? id);
        Task DeleteAysnc(int? id);
        Task<GroupDetailDto> DetailAsync(int? id);
        Task EditAysnc(GroupEditDto model,int? id);
        Task<List<GroupSearchDto>> SearchAsync(string searchText);
        Task<List<GroupDto>> SortByAsync(string text,bool? IsDescending);
        Task DeleteTeacherAsync(int? teacherId, int? groupId);
        Task AddToTeacherAsync(int? teacherId, int? groupId);
        Task<PaginationResponse<GroupDto>> GetPaginateDatasAsync(int page,int take);
    }
}


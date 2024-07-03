using System;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Teachers;

namespace Service.Services.Interfaces
{
	public interface ITeacherService
	{
        Task<List<TeacherDto>> GetAllAsync();
        Task CreateAsync(TeacherCreateDto model);
        Task<TeacherDto> GetByIdAsync(int? id);
        Task DeleteAysnc(int? id);
        Task<TeacherDetailDto> DetailAsync(int? id);
        Task EditAsync(TeacherEditDto model, int? id);
        Task<IEnumerable<TeacherSearchByNameOrSurnameDto>> SearchByNameOrSurnameAsync(string searchText);
        Task<IEnumerable<TeacherDto>> SortByAsync(string text, bool? IsDescending);

    }
}


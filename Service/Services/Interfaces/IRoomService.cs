using System;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Rooms;

namespace Service.Services.Interfaces
{
	public interface IRoomService
	{
		Task CreateAsync(RoomCreateDto model);
		Task<IEnumerable<RoomDto>> GetAllAsync();
		Task<RoomDto> GetByIdAsync(int? id);
		Task DeleteAsync(int? id);
        Task<RoomDetailDto> DetailAsync(int? id);
        Task EditAysnc(RoomEditDto model, int? id);
        Task<IEnumerable<RoomSearchByNameDto>> SearchByNameAsync(string text);
        Task<IEnumerable<RoomDto>> SortByAsync(string text, bool? IsDescending);
    }
}


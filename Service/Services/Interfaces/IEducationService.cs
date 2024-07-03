using System;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;

namespace Service.Services.Interfaces
{
	public interface IEducationService
	{
		Task CreateAsync(EducationCreateDto model);
		Task<IEnumerable<EducationDto>> GetAllAsync();
        Task<EducationDto> GetByIdAsync(int? id);
        Task DeleteAysnc(int? id);
        Task<EducationDetailDto> DetailAsync(int? id);
        Task EditAysnc(EducationEditDto model, int? id);
        Task<IEnumerable<EducationSearchByNameDto>> SearchByNameAsync(string text);
        Task<IEnumerable<EducationDto>> SortByAsync(string text, bool? IsDescending);

    }
}


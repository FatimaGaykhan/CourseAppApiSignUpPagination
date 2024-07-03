using System;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Students;

namespace Service.Services.Interfaces
{
	public interface IStudentService
	{
		Task<List<StudentDto>> GetAllAsync();
		Task CreateAsync(StudentCreateDto model);
		Task DeleteAsync(int? id);
		Task<StudentDto> GetByIdAsync(int? id);
		Task<StudentDetailDto> DetailAsync(int? id);
		Task EditAsync(int? id,StudentEditDto model);
		Task DeleteGroupAsync(int? studentId,int? groupId);
        Task AddToGroupAsync(int? studentId, int? groupId);
        Task<List<StudentSearchByNameOrSurnameDto>> SearchByNameOrSurnameAsync(string searchText);
        Task<IEnumerable<StudentDto>> FilterAsync(string name, string surname, int? age);


    }
}


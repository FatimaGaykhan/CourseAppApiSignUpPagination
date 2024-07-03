using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;
using Service.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Services
{
	public class TeacherService:ITeacherService
	{
        private readonly ITeacherRepository _teacherRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly IGroupTeacherRepository _groupTeacherRepository;
        private readonly IGroupStudentRepository _groupStudentRepo;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepo,
                             IMapper mapper,
                             IGroupRepository groupRepo,
                             IGroupStudentRepository groupStudentRepo,
                              IGroupTeacherRepository groupTeacherRepository)
		{
            _mapper = mapper;
            _teacherRepo = teacherRepo;
            _groupRepo = groupRepo;
            _groupStudentRepo = groupStudentRepo;
            _groupTeacherRepository = groupTeacherRepository;


        }

        

        public async Task CreateAsync(TeacherCreateDto model)
        {
            if (model is null) throw new ArgumentNullException();

            Teacher teacher = _mapper.Map<Teacher>(model);

            await _teacherRepo.CreateAsync(teacher);
        }

        public async Task DeleteAysnc(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existTeacher = await _teacherRepo.GetById((int)id);

            if (existTeacher is null) throw new NullReferenceException();

            await _teacherRepo.DeleteAsync(existTeacher);
        }

        

        public async Task<TeacherDetailDto> DetailAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existTeacher = await _teacherRepo.GetById((int)id);

            if (existTeacher is null) throw new NullReferenceException();

            var data = await _teacherRepo.FindBy(m => m.Id == id, source => source.Include(m => m.GroupTeachers).ThenInclude(m=>m.Group)).FirstOrDefaultAsync();

            return _mapper.Map<TeacherDetailDto>(data);

        }

        public async Task EditAsync(TeacherEditDto model, int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existTeacher = await _teacherRepo.GetById((int)id);

            if (existTeacher is null) throw new NullReferenceException();

            await _teacherRepo.EditAsync(_mapper.Map(model, existTeacher));
        }

        public async Task<List<TeacherDto>> GetAllAsync()
        {
            return _mapper.Map<List<TeacherDto>>(await _teacherRepo.GetAllAsync());
        }

        public async Task<TeacherDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var data = await _teacherRepo.FindBy(m => m.Id == id, source => source.Include(m => m.GroupTeachers)
                                                                           .ThenInclude(m => m.Group)).FirstOrDefaultAsync();

            return _mapper.Map<TeacherDto>(data);
        }

        public async Task<IEnumerable<TeacherSearchByNameOrSurnameDto>> SearchByNameOrSurnameAsync(string searchText)
        {
            if (searchText is null)
            {
                return _mapper.Map<IEnumerable<TeacherSearchByNameOrSurnameDto>>(await _teacherRepo.GetAllAsync());
            }

            var result = await _teacherRepo.FindBy(m => m.Name.Contains(searchText)||m.Surname.Contains(searchText)).ToListAsync();

            return result.Count == 0 ? throw new NullReferenceException("Data not Found") : _mapper.Map<IEnumerable<TeacherSearchByNameOrSurnameDto>>(result);
        }

        public async Task<IEnumerable<TeacherDto>> SortByAsync(string text, bool? IsDescending)
        {
            var result = await _teacherRepo.SortBy(text, (bool)IsDescending);

            return _mapper.Map<IEnumerable<TeacherDto>>(result);
        }
    }
}


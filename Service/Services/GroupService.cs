using System;
using System.Diagnostics.Metrics;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Teachers;
using Service.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Services
{
	public class GroupService:IGroupService
	{

        private readonly IGroupRepository _groupRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IEducationRepository _educationRepo;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepo;
        private readonly IGroupTeacherRepository _groupTeacherRepo;

        public GroupService(IGroupRepository groupRepo,
                            IMapper mapper,
                            IRoomRepository roomRepo,
                            IEducationRepository educationRepo,
                            ITeacherRepository teacherRepo,
                            IGroupTeacherRepository groupTeacherRepo)
		{
            _groupRepo = groupRepo;
            _mapper = mapper;
            _educationRepo = educationRepo;
            _roomRepo = roomRepo;
            _teacherRepo = teacherRepo;
            _groupTeacherRepo = groupTeacherRepo;
		}

        public async Task CreateAsync(GroupCreateDto model)
        {
            

            if (model is null) throw new ArgumentNullException();

            await _groupRepo.CreateAsync(_mapper.Map<Group>(model));
        }

        public async Task DeleteAysnc(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existGroup = await _groupRepo.GetById((int)id);

            if (existGroup is null) throw new NullReferenceException();

            await _groupRepo.DeleteAsync(existGroup);
        }

        public async Task<GroupDetailDto> DetailAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existGroup = await _groupRepo.GetById((int)id);

            if (existGroup is null) throw new NullReferenceException();

            var data = await _groupRepo.FindBy(m => m.Id == id, source => source.Include(m => m.GroupStudents)
                                                                                .ThenInclude(m => m.Student))
                                                                                .Include(m=>m.Room)
                                                                                .Include(m=>m.Education)
                                                                                .FirstOrDefaultAsync();

            return _mapper.Map<GroupDetailDto>(data);
        }

        public async Task EditAysnc(GroupEditDto model, int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existRoom = await _roomRepo.GetById(model.RoomId);

            if (existRoom is null) throw new NullReferenceException("Room Not Found");
       

            var existGroup = await _groupRepo.GetById((int)id);

            if (existGroup is null) throw new NullReferenceException("Group Not Found");

            var existEducation = await _educationRepo.GetById(model.EducationId);

            if (existEducation is null) throw new NullReferenceException("Education Not Found");

            await _groupRepo.EditAsync(_mapper.Map(model, existGroup));

        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GroupDto>>( await _groupRepo.GetAllAsync());
        }

        public async Task<GroupDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existGroup = await _groupRepo.GetById((int)id);

            if (existGroup is null) throw new NullReferenceException();

            return _mapper.Map<GroupDto>(existGroup);
        }

        public async Task<List<GroupSearchDto>> SearchAsync(string searchText)
        {
            var result = await _groupRepo.SearchByName(searchText);

            if (result is null) throw new NullReferenceException();

            return _mapper.Map<List<GroupSearchDto>>(result);
        }

        public async Task<List<GroupDto>> SortByAsync(string text, bool? IsDescending)
        {
            var result = await _groupRepo.SortBy(text, (bool)IsDescending);

            return _mapper.Map<List<GroupDto>>(result);
        }

        public async Task DeleteTeacherAsync(int? teacherId, int? groupId)
        {
            if (teacherId is null) throw new ArgumentNullException(nameof(teacherId));
            if (groupId is null) throw new ArgumentNullException(nameof(groupId));

            var existTeacher = await _teacherRepo.FindBy(m => m.Id == teacherId, source => source.Include(m => m.GroupTeachers)).FirstOrDefaultAsync();
            if (existTeacher == null)
            {
                throw new NullReferenceException("Teacher not found.");
            }

            var groupTeacher = existTeacher.GroupTeachers.FirstOrDefault(m => m.GroupId == groupId);

            if (groupTeacher == null)
            {
                throw new NullReferenceException("GroupTeacher relationship not found.");
            }

            await _groupTeacherRepo.DeleteAsync(groupTeacher);
        }

        public async Task AddToTeacherAsync(int? teacherId, int? groupId)
        {
            if (teacherId is null) throw new ArgumentNullException();

            var existTeacher = await _teacherRepo.FindBy(m => m.Id == teacherId, source => source.Include(m => m.GroupTeachers).ThenInclude(m => m.Group)).FirstOrDefaultAsync();

            var groups = await _groupRepo.GetAllAsync();

            var group = groups.FirstOrDefault(m => m.Id == groupId);

            if (group is null) throw new NullReferenceException();

            var groupTeacher = new GroupTeacher
            {
                TeacherId = existTeacher.Id,
                GroupId = group.Id
            };

            existTeacher.GroupTeachers.Add(groupTeacher);

            await _teacherRepo.EditAsync(existTeacher);
        }

        public async Task<PaginationResponse<GroupDto>> GetPaginateDatasAsync(int page, int take)
        {
            var groups = await _groupRepo.GetAllAsync();

            int totalPage = (int)Math.Ceiling((decimal)groups.Count() / take);

            var mappedDatas= _mapper.Map<IEnumerable<GroupDto>>(await _groupRepo.GetPaginateDatasAsync(page, take));

            return new PaginationResponse<GroupDto>(mappedDatas, totalPage, page);
        }
    }
}


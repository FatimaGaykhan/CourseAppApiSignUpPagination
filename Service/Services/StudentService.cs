using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Students;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class StudentService:IStudentService
	{
        private readonly IStudentRepository _studentRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly IGroupStudentRepository _groupStudentRepo;
        private readonly IMapper _mapper;

		public StudentService(IStudentRepository studentRepo,
                             IMapper mapper,
                             IGroupRepository groupRepo,
                             IGroupStudentRepository groupStudentRepo)
		{
            _mapper = mapper;
            _studentRepo = studentRepo;
            _groupRepo = groupRepo;
            _groupStudentRepo = groupStudentRepo;
		}

        public async Task CreateAsync(StudentCreateDto model)
        {
            if (model is null) throw new ArgumentNullException();

            Student student = _mapper.Map<Student>(model);

            List<GroupStudent> groupStudents = new();
            foreach (var groupId in model.GroupIds)
            {
                groupStudents.Add(new GroupStudent { StudentId = student.Id, GroupId = groupId });
            }

            student.GroupStudents = groupStudents;

            await _studentRepo.CreateAsync(student);
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existStudent = await _studentRepo.GetById((int)id);

            if (existStudent is null) throw new NullReferenceException();

            await _studentRepo.DeleteAsync(existStudent);
        }

        public async Task DeleteGroupAsync(int? studentId, int? groupId)
        {
            if (studentId is null) throw new ArgumentNullException(nameof(studentId));
            if (groupId is null) throw new ArgumentNullException(nameof(groupId));

            var existStudent = await _studentRepo.FindBy(m => m.Id == studentId, source => source.Include(m => m.GroupStudents)).FirstOrDefaultAsync();
            if (existStudent == null)
            {
                throw new NullReferenceException("Student not found.");
            }

            var groupStudent = existStudent.GroupStudents.FirstOrDefault(m => m.GroupId==groupId);

            if (groupStudent == null)
            {
                throw new NullReferenceException("GroupStudent relationship not found.");
            }

            await _groupStudentRepo.DeleteAsync(groupStudent);

        }



        public async Task<StudentDetailDto> DetailAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var data = await _studentRepo.FindBy(m => m.Id == id, source=>source.Include(m=>m.GroupStudents)
                                                                           .ThenInclude(m=>m.Group)).FirstOrDefaultAsync();

            return _mapper.Map<StudentDetailDto>(data);
        }

        public async Task EditAsync(int? id, StudentEditDto model)
        {
            if (id is null) throw new ArgumentNullException();

            var existStudent = await _studentRepo.GetById((int)id);

            if (existStudent is null) throw new NullReferenceException();

            await _studentRepo.EditAsync(_mapper.Map(model, existStudent));
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {
            return _mapper.Map<List<StudentDto>>(await _studentRepo.GetAllAsync());
        }

        public async Task<StudentDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var data = await _studentRepo.FindBy(m => m.Id == id, source => source.Include(m => m.GroupStudents)
                                                                           .ThenInclude(m => m.Group)).FirstOrDefaultAsync();

            return _mapper.Map<StudentDto>(data);
        }


        public async Task<List<StudentSearchByNameOrSurnameDto>> SearchByNameOrSurnameAsync(string searchText)
        {
            var result = await _studentRepo.SearchByNameOrSurname(searchText);

            if (result is null) throw new NullReferenceException();

            return _mapper.Map<List<StudentSearchByNameOrSurnameDto>>(result);
        }

        public async Task<IEnumerable<StudentDto>> FilterAsync(string name, string surname, int? age)
        {
            
            if (surname==null && age==null )
            {
                var result = await _studentRepo.FindBy(m => m.Name.Contains(name)).ToListAsync();
                return _mapper.Map<IEnumerable<StudentDto>>(result);

            }else if (name == null && age == null)
            {
                var result = await _studentRepo.FindBy(m => m.Surname.Contains(surname)).ToListAsync();
                return _mapper.Map<IEnumerable<StudentDto>>(result);

            }else if (name == null && surname == null)
            {
                var result = _studentRepo.FindBy(m => m.Age==age).ToListAsync();
                return _mapper.Map<IEnumerable<StudentDto>>(result);
            }
            else if (name is null)
            {
                var result = await _studentRepo.FindBy(m => m.Surname.Contains(surname) && m.Age == age).ToListAsync();
                return _mapper.Map<IEnumerable<StudentDto>>(result);
            }
            else if (surname is null)
            {
                var result = await _studentRepo.FindBy(m => m.Name.Contains(name) && m.Age == age).ToListAsync();
                return _mapper.Map<IEnumerable<StudentDto>>(result);

            }
            else if (age is null)
            {
                var result = await _studentRepo.FindBy(m => m.Name.Contains(name) && m.Surname.Contains(surname)).ToListAsync();
                return _mapper.Map<IEnumerable<StudentDto>>(result);

            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task AddToGroupAsync(int? studentId, int? groupId)
        {
            if (studentId is null) throw new ArgumentNullException();

            var existStudent = await _studentRepo.FindBy(m => m.Id == studentId, source => source.Include(m => m.GroupStudents).ThenInclude(m => m.Group)).FirstOrDefaultAsync();
            var groupStudents = await _groupStudentRepo.FindBy(m => m.GroupId == groupId).ToListAsync();
            int groupStudentsCount = groupStudents.Count();


            var groups = await _groupRepo.GetAllAsync();

            var group = groups.FirstOrDefault(m=>m.Id==groupId);

            if (group is null) throw new NullReferenceException();

            var groupStudent = new GroupStudent
            {
                StudentId = existStudent.Id,
                GroupId = group.Id
            };

            if (group.Capacity <= groupStudentsCount)
            {
                throw new NullReferenceException($"Capacity is {group.Capacity }");
            }

            existStudent.GroupStudents.Add(groupStudent);

            await _studentRepo.EditAsync(existStudent);
        }
    }
}


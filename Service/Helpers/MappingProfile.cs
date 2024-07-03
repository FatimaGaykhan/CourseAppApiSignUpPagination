using System;
using System.Diagnostics.Metrics;
using AutoMapper;
using Domain.Entities;
using Service.DTOs.Account;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;

namespace Service.Helpers
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<GroupCreateDto, Group>();
			CreateMap<Group, GroupDto>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy")));
			CreateMap<Group, GroupDetailDto>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy")))
								.ForMember(dest => dest.GroupStudents, opt => opt.MapFrom(src => src.GroupStudents.Select(m => m.Student.Name)))
								.ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => new List<string> { src.Room.Name }))
                                .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => new List<string> { src.Education.Name }));
            CreateMap<GroupEditDto, Group>();
			CreateMap<Group, GroupSearchDto>();


			CreateMap<Student, StudentDto>();
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentDetailDto>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.StudentGroups, opt => opt.MapFrom(src => src.GroupStudents.Select(m=>m.Group.Name)));

			CreateMap<StudentEditDto, Student>();
            CreateMap<Student, StudentSearchByNameOrSurnameDto>();

			CreateMap<EducationCreateDto, Education>();
			CreateMap<Education, EducationDto>();
			CreateMap<EducationEditDto, Education>();
			CreateMap<Education, EducationDetailDto>().ForMember(dest=>dest.CreateDate, opt=>opt.MapFrom(src=>src.CreatedDate.ToString("dd.MM.yyyy")))
				 .ForMember(dest=>dest.Groups,opt=>opt.MapFrom(src=>src.Groups.Select(m=>m.Name)));
			CreateMap<Education, EducationSearchByNameDto>();

			CreateMap<Room, RoomDto>();
			CreateMap<RoomCreateDto, Room>();
            CreateMap<Room, RoomDetailDto>().ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy")))
                 .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups.Select(m => m.Name)));
            CreateMap<RoomEditDto, Room>();
            CreateMap<Room, RoomSearchByNameDto>();


			CreateMap<TeacherCreateDto, Teacher>().ForMember(dest => dest.Salary,opt=>opt.MapFrom(src=> Convert.ToDecimal(src.Salary)));
			CreateMap<Teacher, TeacherDto>().ForMember(dest => dest.Salary, opt => opt.MapFrom(src => Convert.ToString(src.Salary)));
			CreateMap<Teacher, TeacherDetailDto>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy")))
				.ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupTeachers.Select(m => m.Group.Name)))
				.ForMember(dest => dest.Salary, opt => opt.MapFrom(src => Convert.ToString(src.Salary)));

			CreateMap<TeacherEditDto,Teacher>().ForMember(dest => dest.Salary, opt => opt.MapFrom(src => Convert.ToDecimal(src.Salary)));
			CreateMap<Teacher,TeacherSearchByNameOrSurnameDto>().ForMember(dest => dest.Salary, opt => opt.MapFrom(src => Convert.ToString(src.Salary)));

			CreateMap<RegisterDto, AppUser>();
			CreateMap<AppUser, UserDto>();
        }
	}
}


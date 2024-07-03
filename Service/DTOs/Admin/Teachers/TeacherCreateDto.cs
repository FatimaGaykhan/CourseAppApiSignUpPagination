using System;
using FluentValidation;
using Service.DTOs.Admin.Students;

namespace Service.DTOs.Admin.Teachers
{
	public class TeacherCreateDto
	{
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Salary { get; set; }

        public int Age { get; set; }


    }

    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("Surname is required");
            RuleFor(x => x.Email).EmailAddress().NotNull().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Salary).NotNull().WithMessage("Salary is required");
            RuleFor(x => x.Age).NotNull().WithMessage("Age is required");





        }
    }
}


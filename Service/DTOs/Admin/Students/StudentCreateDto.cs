using System;
using FluentValidation;
using Service.DTOs.Admin.Groups;

namespace Service.DTOs.Admin.Students
{
	public class StudentCreateDto
	{
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public List<int> GroupIds { get; set; }
    }

    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("Surname is required");
            RuleFor(x => x.Email).EmailAddress().NotNull().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required");
            RuleFor(x => x.Age).NotNull().WithMessage("Age is required");





        }
    }
}


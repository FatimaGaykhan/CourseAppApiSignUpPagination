using System;
using FluentValidation;

namespace Service.DTOs.Admin.Students
{
	public class StudentEditDto
	{
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
    }

    public class StudentEditDtoValidator : AbstractValidator<StudentEditDto>
    {
        public StudentEditDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("Surname is required");
            RuleFor(x => x.Email).EmailAddress().NotNull().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required");
            RuleFor(x => x.Age).NotNull().WithMessage("Age is required");





        }
    }
}


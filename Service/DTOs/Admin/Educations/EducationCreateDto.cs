using System;
using FluentValidation;
using Service.DTOs.Admin.Groups;

namespace Service.DTOs.Admin.Educations
{
	public class EducationCreateDto
	{
		public string  Name { get; set; }

	}
    public class EducationCreateDtoValidator : AbstractValidator<EducationCreateDto>
    {
        public EducationCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");

        }
    }
}


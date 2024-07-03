using System;
using FluentValidation;

namespace Service.DTOs.Admin.Educations
{
	public class EducationEditDto
	{
		public string Name { get; set; }

	}

    public class EducationEditDtoValidator : AbstractValidator<EducationEditDto>
    {
        public EducationEditDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");

        }
    }
}


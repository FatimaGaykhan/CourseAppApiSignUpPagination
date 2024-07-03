using System;
using FluentValidation;

namespace Service.DTOs.Admin.Groups
{
	public class GroupEditDto
	{
        public string Name { get; set; }

        public int Capacity { get; set; }

        public int RoomId { get; set; }

        public int EducationId { get; set; }

    }

    public class GroupEditDtoValidator : AbstractValidator<GroupEditDto>
    {
        public GroupEditDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Capacity).NotNull().WithMessage("Capacity is required");
            RuleFor(x => x.RoomId).NotNull().WithMessage("RoomId is required");
            RuleFor(x => x.EducationId).NotNull().WithMessage("EducationId is required");



        }
    }
}


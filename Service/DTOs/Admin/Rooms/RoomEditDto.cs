using System;
using FluentValidation;

namespace Service.DTOs.Admin.Rooms
{
	public class RoomEditDto
	{
        public string Name { get; set; }

        public int SeatCount { get; set; }
    }

    public class RoomEditDtoValidator : AbstractValidator<RoomEditDto>
    {
        public RoomEditDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");

            RuleFor(x => x.SeatCount).NotNull().WithMessage("SeatCount is required");


        }
    }
}


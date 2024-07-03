using System;
using FluentValidation;
using Service.DTOs.Admin.Educations;

namespace Service.DTOs.Admin.Rooms
{
	public class RoomCreateDto
	{
		public string Name { get; set; }

		public int SeatCount { get; set; }

	}

    public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");

            RuleFor(x => x.SeatCount).NotNull().WithMessage("SeatCount is required");


        }
    }
}


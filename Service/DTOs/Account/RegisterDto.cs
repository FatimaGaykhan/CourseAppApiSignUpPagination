using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Service.DTOs.Admin.Groups;

namespace Service.DTOs.Account
{
	public class RegisterDto
	{
		public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }


        public string Email { get; set; }

	}

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Username is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email format is wrong").NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

        }
    }
}


using System;
using Domain.Entities;

namespace Service.DTOs.Admin.Groups
{
	public class GroupStudentDto
	{
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int StudentId { get; set; }
    }
}


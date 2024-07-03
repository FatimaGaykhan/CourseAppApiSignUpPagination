using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Surname).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Salary).IsRequired();
            builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Age).IsRequired();
        }
    }
}


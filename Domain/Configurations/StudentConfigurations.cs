using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Surname).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(300);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Age).IsRequired();



        }
    }
}


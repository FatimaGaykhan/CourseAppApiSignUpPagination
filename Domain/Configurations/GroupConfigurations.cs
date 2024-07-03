using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
	public class GroupConfigurations: IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Capacity).IsRequired();
            builder.Property(e => e.EducationId).IsRequired();
            builder.Property(e => e.RoomId).IsRequired();


        }
    }
}


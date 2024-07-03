using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.SeatCount).IsRequired();
        }
    }
}


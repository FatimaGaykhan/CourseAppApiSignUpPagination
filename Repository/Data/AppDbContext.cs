using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
	public class AppDbContext:IdentityDbContext<AppUser>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<GroupStudent> GroupStudents { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<GroupTeacher> GroupTeachers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
    }
}


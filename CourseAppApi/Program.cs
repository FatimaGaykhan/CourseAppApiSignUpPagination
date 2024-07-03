 using System;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository;
using Service;
using CourseAppApi.Middlewares;
using Serilog;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var logger = new LoggerConfiguration()

    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(context =>
{
    context.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        opt =>

            opt.MigrationsAssembly("Repository")
    );

});

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.User.RequireUniqueEmail = true;

});



builder.Services.AddRepositoryLayer();
builder.Services.AddServiceLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


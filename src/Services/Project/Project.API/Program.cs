using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Interfaces;
using Project.API.Profiles;
using Project.API.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new ProjectProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add Database
var connectionStringBuilder = new SqlConnectionStringBuilder
{
    Password = builder.Configuration["Database:Password"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_PASSWORD"),
    ["Server"] = builder.Configuration["Database:Server"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_SERVER"),
    ["Database"] = builder.Configuration["Database:DatabaseName"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_NAME"),
    UserID = builder.Configuration["Database:UserId"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_USER")
};

builder.Services.AddDbContext<ProjectContext>(opt =>
{
    opt.UseSqlServer(connectionStringBuilder.ConnectionString);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IProjectProvider, ProjectProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
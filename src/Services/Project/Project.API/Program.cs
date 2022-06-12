using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Profiles;

var builder = WebApplication.CreateBuilder(args);

var dbPassword = builder.Configuration["Database:Password"];
var connection = $"Server=db;Database=master;User=sa;Password={dbPassword};";

// Add AutoMapper
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new ProjectProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ProjectContext>(opt =>
{
    opt.UseSqlServer(connection);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
using Microsoft.Net.Http.Headers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Ocelot
builder.WebHost.ConfigureAppConfiguration((_, config) =>
{
    config.AddJsonFile("ocelot.json");
});

builder.Services.AddOcelot();

var app = builder.Build();

// Use Ocelot
app.UseOcelot().Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseCors(policy => 
    policy.WithOrigins("https://localhost:3001", "http://localhost:3000")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
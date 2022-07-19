using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.API.Database;
using Project.API.Interfaces;
using Project.API.Profiles;
using Project.API.Providers;

var builder = WebApplication.CreateBuilder(args);

var identityHostname = builder.Configuration["Identity:Hostname"] ??
                       Environment.GetEnvironmentVariable("IDENTITY_HOSTNAME");

// Add AutoMapper
var mapperConfig = new MapperConfiguration(config => { config.AddProfile(new ProjectProfile()); });
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add Database
var connectionStringBuilder = new SqlConnectionStringBuilder
{
    Password = builder.Configuration["Database:Password"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_PASSWORD"),
    ["Server"] = builder.Configuration["Database:Server"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_SERVER"),
    ["Database"] = builder.Configuration["Database:DatabaseName"] ??
                   Environment.GetEnvironmentVariable("PROJECT_DB_NAME"),
    UserID = builder.Configuration["Database:UserId"] ?? Environment.GetEnvironmentVariable("PROJECT_DB_USER")
};

builder.Services.AddDbContext<ProjectContext>(opt => { opt.UseSqlServer(connectionStringBuilder.ConnectionString); });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IProjectProvider, ProjectProvider>();

// Secure access via Identity JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = identityHostname;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "readProjectApi");
    });
});

// Add auth via swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"SampleAPI", "API - full access"}
                }
            },
        }
    });

    // Apply Scheme globally
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
            },
            new[] {"SampleAPI"}
        }
    });
});

var app = builder.Build();

// Apply migrations
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    if (serviceScope != null)
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<ProjectContext>();
        context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthUsePkce();
    });
}

app.MapControllers().RequireAuthorization("ApiScope");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
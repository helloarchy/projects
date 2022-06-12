using Microsoft.EntityFrameworkCore;

namespace Project.API.Database;

public class ProjectContext : DbContext
{
    public DbSet<Models.Project>? Projects { get; set; }
    
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    {
    }
}
using Microsoft.EntityFrameworkCore;

namespace Projects.Models;

public class ProjectContext : DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    {
    }
        
    public DbSet<Project> Projects { get; set; }
}
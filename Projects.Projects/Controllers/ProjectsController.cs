using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Models;

namespace Projects.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly ProjectContext _context;

    public ProjectsController(ProjectContext context)
    {
        _context = context;
    }

    /**
         * Convert a given Project to a Project DTO where only fields suitable for data transfer are included
         */
    private static ProjectDTO ToProjectDto(Project project) =>
        new()
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            IsComplete = project.IsComplete
        };
        
    private bool ProjectExists(long id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }


    // GET: api/projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
    {
        return await _context.Projects.Select(project => ToProjectDto(project)).ToListAsync();
    }

    // GET: api/projects/5
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProjectDTO>> GetProject(long id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return ToProjectDto(project);
    }

    // PUT: api/projects/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutProject(long id, ProjectDTO projectDto)
    {
        if (id != projectDto.Id)
        {
            return BadRequest();
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        project.Title = projectDto.Title;
        project.Description = projectDto.Description;
        project.IsComplete = projectDto.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ProjectExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/projects
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> PostProject(ProjectDTO projectDto)
    {
        var project = new Project
        {
            Title = projectDto.Title,
            Description = projectDto.Description,
            IsComplete = projectDto.IsComplete
        };
            
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProject), 
            new { id = project.Id }, 
            ToProjectDto(project)
        );
    }

    // DELETE: api/projects/5
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteProject(long id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
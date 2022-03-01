using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.API.Models;

namespace Project.API.Controllers
{
    [Route("[controller]")]
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
        private static ProjectDto ToProjectDto(Models.Project project) =>
            new()
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                IsComplete = project.IsComplete
            };

        // GET: api/Projects
        [HttpGet]
        public async Task<List<ProjectDto>> GetProjects()
        {
            return await _context.Projects
                .Select(x => ToProjectDto(x))
                .ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return ToProjectDto(project);
        }

        // PUT: api/Projects/5
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(long id, ProjectDto projectDto)
        {
            if (id != projectDto.Id)
            {
                return BadRequest();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project is null)
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

        // POST: api/Projects
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto)
        {
            var project = new Models.Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                IsComplete = projectDto.IsComplete
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProject),
                new {id = project.Id},
                ToProjectDto(project));
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
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

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
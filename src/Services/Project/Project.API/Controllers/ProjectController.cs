using Microsoft.AspNetCore.Mvc;
using Project.API.Interfaces;
using Project.API.Models;

namespace Project.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectProvider _projectProvider;

        public ProjectController(IProjectProvider projectProvider)
        {
            _projectProvider = projectProvider;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var result = await _projectProvider.GetProjectsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Projects);
            }

            return NotFound();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
        {
            var result = await _projectProvider.GetProjectAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Project);
            }

            return NotFound();
        }
        
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, ProjectDto projectDto)
        {
            if (id != projectDto.Id)
            {
                return BadRequest("The project ID does not match the ID of the project provided");
            }
            
            if (!await _projectProvider.ProjectExists(id))
            {
                return NotFound();
            }
            
            var result = await _projectProvider.UpdateProjectAsync(id, projectDto);
            if (result.IsSuccess)
            {
                return Ok();
            }

            return NotFound();
        }
        
        // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto)
        {
            var result = await _projectProvider.CreateProjectAsync(projectDto);
            if (result.IsSuccess && result.Project != null)
            {
                return CreatedAtAction(
                    nameof(GetProject),
                    new {id = result.Project.Id},
                    result.Project);
            }

            return BadRequest(result.ErrorMessage);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var result = await _projectProvider.DeleteProjectAsync(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
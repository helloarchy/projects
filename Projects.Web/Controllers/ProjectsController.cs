using Microsoft.AspNetCore.Mvc;
using Projects.Web.Interfaces;

namespace Projects.Web.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IProjectsService _projectsService;

    public ProjectsController(ILogger<ProjectsController> logger, IProjectsService projectsService)
    {
        _logger = logger;
        _projectsService = projectsService;
    }

    [HttpGet("/api/projects")]
    public IActionResult GetAllProjects()
    {
        return Ok("All projects...");
    }

    [HttpGet("/api/projects/{id:int}")]
    public IActionResult GetSingleProject(int id)
    {
        return Ok("Some project...");
    }
    
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.API.Database;
using Project.API.Interfaces;
using Project.API.Models;

namespace Project.API.Providers;

public class ProjectProvider : IProjectProvider
{
    private readonly ProjectContext _context;
    private readonly ILogger<ProjectProvider> _logger;
    private readonly IMapper _mapper;

    public ProjectProvider(ProjectContext context, ILogger<ProjectProvider> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<(bool IsSuccess, IEnumerable<ProjectDto> Projects, string ErrorMessage)> GetProjectsAsync()
    {
        try
        {
            if (_context.Projects != null)
            {
                var projects = await _context.Projects.ToListAsync();
                var mappedProjects = _mapper.Map<IEnumerable<Models.Project>, IEnumerable<ProjectDto>>(projects);
                return (true, mappedProjects, string.Empty);
            }

            return (false, Enumerable.Empty<ProjectDto>(), "Not found");
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, Enumerable.Empty<ProjectDto>(), e.Message);
        }
    }

    public Task<(bool IsSuccess, ProjectDto Product, string ErrorMessage)> GetProjectAsync(int id)
    {
        throw new NotImplementedException();
    }
}
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
            _logger.LogError(e.ToString());
            return (false, Enumerable.Empty<ProjectDto>(), e.Message);
        }
    }

    public async Task<(bool IsSuccess, ProjectDto? Project, string ErrorMessage)> GetProjectAsync(Guid id)
    {
        try
        {
            if (_context.Projects != null)
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                {
                    var mappedDto = _mapper.Map<Models.Project, ProjectDto>(project);
                    return (true, mappedDto, string.Empty);
                }
            }

            return (false, null, "Not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return (false, null, e.Message);
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> UpdateProjectAsync(Guid id, ProjectDto projectDto)
    {
        try
        {
            if (_context.Projects != null)
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                {
                    // Update the project
                    project.Title = projectDto.Title;
                    project.Description = projectDto.Description;
                    project.IsComplete = projectDto.IsComplete;

                    await _context.SaveChangesAsync();
                    
                    return (true, string.Empty);
                }
            }

            return (false, "Not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return (false, e.Message);
        }
    }

    public async Task<(bool IsSuccess, ProjectDto? Project, string ErrorMessage)> CreateProjectAsync(ProjectDto projectDto)
    {
        try
        {
            if (_context.Projects == null) {
                return (false, null, "There was an error creating the project");
            }
            
            var project = new Models.Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                IsComplete = projectDto.IsComplete
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            var mappedDto = _mapper.Map<Models.Project, ProjectDto>(project);
            return (true, mappedDto, string.Empty);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return (false, null, e.ToString());
        }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProjectAsync(Guid id)
    {
        try
        {
            if (_context.Projects != null)
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
                if (project != null)
                {
                    _context.Remove(project);
                    await _context.SaveChangesAsync();
                    
                    return (true, string.Empty);
                }
            }

            return (false, "Not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return (false, e.Message);
        }
    }

    public async Task<bool> ProjectExists(Guid id)
    {
        return _context.Projects != null && await _context.Projects.AnyAsync(e => e.Id == id);
    }
}
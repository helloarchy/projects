using Project.API.Models;

namespace Project.API.Interfaces;

public interface IProjectProvider
{
    Task<(bool IsSuccess, IEnumerable<ProjectDto> Projects, string ErrorMessage)> GetProjectsAsync();
    Task<(bool IsSuccess, ProjectDto? Project, string ErrorMessage)> GetProjectAsync(Guid id);
    Task<(bool IsSuccess, string ErrorMessage)> UpdateProjectAsync(Guid id, ProjectDto projectDto);
    Task<(bool IsSuccess, ProjectDto? Project, string ErrorMessage)> CreateProjectAsync(ProjectDto projectDto);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteProjectAsync(Guid id);
    Task<bool> ProjectExists(Guid id);
}
using Project.API.Models;

namespace Project.API.Interfaces;

public interface IProjectProvider
{
    Task<(bool IsSuccess, IEnumerable<ProjectDto> Projects, string ErrorMessage)> GetProjectsAsync();
    Task<(bool IsSuccess, ProjectDto Product, string ErrorMessage)> GetProjectAsync(int id);
}
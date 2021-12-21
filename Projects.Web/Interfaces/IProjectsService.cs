namespace Projects.Web.Interfaces;

public interface IProjectsService
{
    Task<(bool IsSuccess, IEnumerable<Project> Projects, string ErrorMessage)> GetProjectsAsync();
    Task<(bool IsSuccess, Project Project, string ErrorMessage)> GetProjectAsync();
}
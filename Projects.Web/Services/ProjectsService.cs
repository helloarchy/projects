


using System.Text.Json;
using Projects.Web.Interfaces;

namespace Projects.Web.Services;

public class ProjectsService : IProjectsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ProjectsService> _logger;

    public ProjectsService(IHttpClientFactory httpClientFactory, ILogger<ProjectsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, IEnumerable<Project> Projects, string ErrorMessage)> GetProjectsAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ProjectsService");
            var response = await client.GetAsync("api/projects");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<IEnumerable<Project>>(content, options);
                return (true, result, null);
            }

            return (false, null, response.ReasonPhrase);
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
        }

    }

    public Task<(bool IsSuccess, Project Project, string ErrorMessage)> GetProjectAsync()
    {
        throw new NotImplementedException();
    }
}
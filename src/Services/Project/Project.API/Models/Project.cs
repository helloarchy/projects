namespace Project.API.Models;

public class Project
{
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public bool IsComplete { get; set; }
    public string? SomeSecretField { get; set; }
}
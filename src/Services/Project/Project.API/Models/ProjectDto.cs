namespace Project.API.Models;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public bool IsComplete { get; set; }
    public string ImageSource { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string FullDescriptionMdx { get; set; } = string.Empty;
}
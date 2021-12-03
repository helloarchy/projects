using System;

namespace Projects.Models;

public class Project
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public bool IsComplete { get; set; }
    public string SomeSecretField { get; set; }
}
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
        
        SeedData();
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
                    project.IsComplete = projectDto.IsComplete;
                    project.Created = projectDto.Created;
                    project.ImageSource = projectDto.ImageSource;
                    project.ShortDescription = projectDto.ShortDescription;
                    project.FullDescriptionMdx = projectDto.FullDescriptionMdx;

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
                IsComplete = projectDto.IsComplete,
                Created = projectDto.Created,
                ImageSource = projectDto.ImageSource,
                ShortDescription = projectDto.ShortDescription,
                FullDescriptionMdx = projectDto.FullDescriptionMdx
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
    
    private void SeedData()
    {
        if (_context.Projects != null && _context.Projects.Any())
        {
            return;
        }

        var firstProject = new Models.Project
        {
            Title = "First Project",
            Created = DateTime.Now,
            IsComplete = true,
            ImageSource = "https://cataas.com/cat",
            ShortDescription = "Lorem ipsum dolor sit amet",
            FullDescriptionMdx = @"# Heading (rank 1)
## Heading 2
### 3
#### 4
##### 5
###### 6

> Block quote

* Unordered
* List

1. Ordered
2. List

A paragraph, introducing a thematic break:

---

```js
some.code()
```

a [link](https://example.com), an ![image](./image.png), some *emphasis*,
something **strong**, and finally a little `code()`."
        };
        
        var secondProject = new Models.Project
        {
            Title = "Second Project",
            Created = DateTime.Now,
            IsComplete = true,
            ImageSource = "https://cataas.com/cat",
            ShortDescription = "Lorem ipsum dolor sit amet",
            FullDescriptionMdx = @"# Heading (rank 1)
## Heading 2
### 3
#### 4
##### 5
###### 6

> Block quote

* Unordered
* List

1. Ordered
2. List

A paragraph, introducing a thematic break:

---

```js
some.code()
```

a [link](https://example.com), an ![image](./image.png), some *emphasis*,
something **strong**, and finally a little `code()`."
        };
        
        var thirdProject = new Models.Project
        {
            Title = "Third Project",
            Created = DateTime.Now,
            IsComplete = true,
            ImageSource = "https://cataas.com/cat",
            ShortDescription = "Lorem ipsum dolor sit amet",
            FullDescriptionMdx = @"# Heading (rank 1)
## Heading 2
### 3
#### 4
##### 5
###### 6

> Block quote

* Unordered
* List

1. Ordered
2. List

A paragraph, introducing a thematic break:

---

```js
some.code()
```

a [link](https://example.com), an ![image](./image.png), some *emphasis*,
something **strong**, and finally a little `code()`."
        };

        if (_context.Projects != null)
        {
            _context.Projects.Add(firstProject);
            _context.Projects.Add(secondProject);
            _context.Projects.Add(thirdProject);

            _context.SaveChanges();
        }
    }
}
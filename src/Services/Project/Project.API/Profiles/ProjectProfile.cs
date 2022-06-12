using AutoMapper;
using Project.API.Models;

namespace Project.API.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Models.Project, ProjectDto>().ReverseMap();
    }
}
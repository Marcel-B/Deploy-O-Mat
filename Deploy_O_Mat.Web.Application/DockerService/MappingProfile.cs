using AutoMapper;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.DockerService, ListDockerServiceDto>();
        }
    }
}
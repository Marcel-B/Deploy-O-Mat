using System.Linq;
using AutoMapper;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.DockerActiveService, DockerActiveServiceDto>();

            CreateMap<Domain.Models.DockerImage, DockerImageDto>();
         
            CreateMap<Domain.Models.DockerStack, DockerStackDto>()
                .ForMember(dest => dest.DockerStackImages, opt => opt.MapFrom(source => source.DockerStackImages.Select(p => p.DockerImage)));
            CreateMap<Domain.Models.DockerService, DockerServiceDto>();
        }
    }
}

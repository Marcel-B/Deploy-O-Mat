using AutoMapper;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.ProcessModels;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Spec, DockerServiceDetail>();
            CreateMap<InspectService, DockerServiceDetail>()
                .ForMember(m => m.Name, o => o.MapFrom(i => i.Spec.Name));
        }
    }
}
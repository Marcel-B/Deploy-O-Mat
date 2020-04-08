using AutoMapper;
using com.b_velop.Deploy_O_Mat.Domain;
using com.b_velop.Utilities.Extensions;

namespace com.b_velop.Deploy_O_Mat.Application.Images
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrUpdate.Command, DockerImage>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Repository.Name))
                .ForMember(dest => dest.Tag, opt =>
                opt.MapFrom(src => src.PushData.Tag))
                .ForMember(dest => dest.Pusher, opt =>
                opt.MapFrom(src => src.PushData.Pusher))
                .ForMember(dest => dest.Namespace, opt =>
                opt.MapFrom(src => src.Repository.Namespace))
                .ForMember(dest => dest.Owner, opt =>
                opt.MapFrom(src => src.Repository.Owner))
                .ForMember(dest => dest.RepoName, opt =>
                opt.MapFrom(src => src.Repository.RepoName))
                .ForMember(dest => dest.RepoUrl, opt =>
                opt.MapFrom(src => src.Repository.RepoUrl))
                .ForMember(dest => dest.Updated, opt =>
                opt.MapFrom(src => ((int)src.PushData.PushedAt).ToDateTime()))
                .ForMember(dest => dest.Description, opt=> opt.MapFrom(src => src.Repository.Description))
                .ForMember(dest => dest.Dockerfile, opt=> opt.MapFrom(src => src.Repository.Dockerfile))
                .ForMember(dest => dest.FullDescription, opt=> opt.MapFrom(src => src.Repository.FullDescription))
                .ForMember(dest => dest.Stars, opt=> opt.MapFrom(src => src.Repository.StarsCount))
                .ForMember(dest => dest.IsOfficial, opt=> opt.MapFrom(src => src.Repository.IsOfficial))
                .ForMember(dest => dest.IsPrivate, opt=> opt.MapFrom(src => src.Repository.IsPrivate))
                .ForMember(dest => dest.Status, opt=> opt.MapFrom(src => src.Repository.Status))
                .ForMember(dest => dest.Created,opt =>
                opt.MapFrom(src => ((int)src.Repository.DateCreated).ToDateTime()));
        }
    }
}

using AutoMapper;

namespace Api.Models;

public class ExceptionDto
{
    public string Canonical { get; set; }
    public string? Exception { get; set; }

    public static void ConfigureMapping(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Exception, ExceptionDto>()
            .ForMember(dest => dest.Canonical, act => act.MapFrom(src => src.Message))
            .ForMember(dest => dest.Exception, act => act.MapFrom(src => src.InnerException));
    }
}
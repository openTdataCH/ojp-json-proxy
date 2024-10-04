using AutoMapper;
using TransportProxy.Application.LocationInformation;
using TransportProxy.Application.LocationInformation.V1;

namespace Api.Models;

public class LocationInformationDto
{
    public List<LocationDto> Locations { get; set; }

    public static void ConfigureMapping(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.Id,
                act => act.MapFrom(src => src.StopPlace.StopPlaceRef))
            .ForMember(dest => dest.Name,
                act => act.MapFrom(src => src.StopPlace.StopPlaceName.Text));

        cfg.CreateMap<LocationWrapper, LocationDto>()
            .IncludeMembers(src => src.Location)
            .ForMember(dest => dest.TransportType,
                act => act.MapFrom(src => string.Join(",", src.Modes.Select(m => m.PtMode))))
            .ForMember(dest => dest.Probability,
                act => act.MapFrom(src => src.Probability));
        
        cfg.CreateMap<OjpLocationInformation, LocationInformationDto>()
            .ForMember(dest => dest.Locations,
                act => act.MapFrom(src =>
                    src.OJPResponse.ServiceDelivery.OJPLocationInformationDelivery.Locations));
    }
}

public class LocationDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TransportType { get; set; }
    public double Probability { get; set; }
}
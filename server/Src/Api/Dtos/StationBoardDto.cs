using AutoMapper;
using TransportProxy.Application.StopEvent;

namespace Api.Models;

public class StationBoardDto
{
    public List<StationBoardJourneyDto> StationBoard { get; set; }

    public static void ConfigureMapping(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<CallAtStop, StationBoardLocationDto>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.StopPointRef))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.StopPointName.Text));
            
        cfg.CreateMap<ThisCall, StationBoardStopDto>()
            .ForMember(dest => dest.Platform, act => act.MapFrom(src => src.CallAtStop.PlannedQuay.Text))
            .ForMember(dest => dest.Arrival,
                act => act.MapFrom(src => src.CallAtStop.ServiceArrival.TimetabledTime ?? ""))
            .ForMember(dest => dest.Departure,
                act => act.MapFrom(src => src.CallAtStop.ServiceDeparture.TimetabledTime ?? ""));
                
        // coordinates & score

        cfg.CreateMap<StopEventResult, StationBoardJourneyDto>()
            .ForMember(dest => dest.Name,
                act => act.MapFrom(src => src.StopEvent.Service.PublishedLineName.Text))
            .ForMember(dest => dest.Operator,
                act => act.MapFrom(src => src.StopEvent.Extension.OperatorName.Text))
            .ForMember(dest => dest.Number,
                act => act.MapFrom(src => src.StopEvent.Extension.PublishedJourneyNumber.Text))
            .ForMember(dest => dest.Category,
                act => act.MapFrom(src => src.StopEvent.Service.Mode.PtMode))
            .ForMember(dest => dest.To,
                act => act.MapFrom(src => src.StopEvent.Service.DestinationText.Text))
            .ForMember(dest => dest.Stop,
                act => act.MapFrom(src => src.StopEvent.Calls.OfType<ThisCall>().FirstOrDefault()));

        cfg.CreateMap<OjpStopEvent, StationBoardDto>()
            .ForMember(dest => dest.StationBoard,
                act => act.MapFrom(src => src.OJPResponse.ServiceDelivery.OJPStopEventDelivery.StopEventResults));
    }
}

public class StationBoardJourneyDto
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Number { get; set; }
    public string Operator { get; set; }
    public string To { get; set; }
    public StationBoardStopDto Stop { get; set; }
}

public class StationBoardStopDto
{
    public StationBoardLocationDto station { get; set; }
    public string? Arrival { get; set; }
    public string? Departure { get; set; }
    public int? Delay { get; set; }
    public string? Platform { get; set; }
    public StationBoardPrognosisDto? Prognosis { get; set; }
}

public class StationBoardLocationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Score { get; set; }
    public StationBoardCoordinates Coordinates { get; set; }
}

public class StationBoardCoordinates
{
    public double x { get; set; }
    public double y { get; set; }
}

public class StationBoardPrognosisDto
{
    public string? Platform { get; set; }
    public string? Arrival { get; set; }
    public string? Departure { get; set; }
    public string? Capacity1st { get; set; }
    public string? Capacity2nd { get; set; }
}
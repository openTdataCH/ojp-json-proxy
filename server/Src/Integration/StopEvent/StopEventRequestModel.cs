namespace TransportProxy.Application.StopEvent;

public class StopEventRequestModel
{
    public string Now { get; set; }
    public string Datetime { get; set; }
    public string Location { get; set; }
    public int Stopref { get; set; }
    public string Prevcalls { get; set; }
    public string Onwardcalls { get; set; }
    public StopEventType Type { get; set; }
    public StopEventMode Mode { get; set; }
    public int Limit { get; set; }
}

public enum StopEventType
{
    departure,
    arrival,
    both
}

public enum StopEventMode
{
    bus,
    train,
    tram,
    ship,
    cableway
}
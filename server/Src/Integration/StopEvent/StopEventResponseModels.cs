using System.Xml.Serialization;

namespace TransportProxy.Application.StopEvent;

// Root class representing the <siri:OJP> node
[XmlRoot("OJP", Namespace = "http://www.siri.org.uk/siri")]
public class OjpStopEvent
{
    [XmlElement("OJPResponse")]
    public OjpResponse OJPResponse { get; set; }
}

// Class representing the <siri:OJPResponse> node
public class OjpResponse
{
    [XmlElement("ServiceDelivery")]
    public ServiceDelivery ServiceDelivery { get; set; }
}

// Class representing the <siri:ServiceDelivery> node
public class ServiceDelivery
{
    [XmlElement("ResponseTimestamp")]
    public string ResponseTimestamp { get; set; }

    [XmlElement("ProducerRef")]
    public string ProducerRef { get; set; }

    [XmlElement("ResponseMessageIdentifier")]
    public string ResponseMessageIdentifier { get; set; }

    [XmlElement("Status")]
    public string Status { get; set; }

    [XmlElement("OJPStopEventDelivery", Namespace = "http://www.vdv.de/ojp")]
    public OjpStopEventDelivery OJPStopEventDelivery { get; set; }
}

// Class representing the <ojp:OJPStopEventDelivery> node
[XmlRoot("OJPStopEventDelivery", Namespace = "http://www.vdv.de/ojp")]
public class OjpStopEventDelivery
{
    [XmlElement("ResponseTimestamp")]
    public string ResponseTimestamp { get; set; }

    [XmlElement("Status")]
    public string Status { get; set; }

    [XmlElement("CalcTime")]
    public string CalcTime { get; set; }

    [XmlElement("StopEventResponseContext")]
    public StopEventResponseContext StopEventResponseContext { get; set; }
    
    [XmlElement("StopEventResult")]
    public List<StopEventResult> StopEventResults { get; set; }
}

// Class representing the <ojp:StopEventResponseContext> node
public class StopEventResponseContext
{
    [XmlArray("Places")]
    [XmlArrayItem("Location")]
    public List<Location> Places { get; set; }
}

// Class representing the <ojp:Location> node
public class Location
{
    [XmlElement("StopPlace")]
    public StopPlace? StopPlace { get; set; }
    
    [XmlElement("StopPoint")]
    public StopPoint? StopPoint { get; set; }

    [XmlElement("PrivateCode")]
    public PrivateCode PrivateCode { get; set; }

    [XmlElement("LocationName")]
    public TextNode LocationName { get; set; }

    [XmlElement("GeoPosition")]
    public GeoPosition GeoPosition { get; set; }
}

public class StopPlace
{
    [XmlElement("StopPlaceRef")]
    public string StopPlaceRef { get; set; }

    [XmlElement("StopPlaceName")]
    public TextNode StopPlaceName { get; set; }
}

public class StopPoint
{
    [XmlElement("StopPointName")]
    public TextNode StopPointName { get; set; }
}


// Class representing the <ojp:PrivateCode> node
public class PrivateCode
{
    [XmlElement("System")]
    public string System { get; set; }

    [XmlElement("Value")]
    public string Value { get; set; }
}

// Class representing the <ojp:GeoPosition> node
public class GeoPosition
{
    [XmlElement("Longitude")]
    public double Longitude { get; set; }

    [XmlElement("Latitude")]
    public double Latitude { get; set; }
}

public class StopEventResult
{
    [XmlElement("ResultId")]
    public string ResultId { get; set; }
    
    [XmlElement("StopEvent")]
    public StopEvent StopEvent { get; set; }
    
    /*[XmlArray("StopEvent")]
    [XmlArrayItem("PreviousCall", typeof(PreviousCall))]
    [XmlArrayItem("OnwardCall", typeof(OnwardCall))]
    [XmlArrayItem("ThisCall", typeof(ThisCall))]
    public List<Call> Calls { get; set; }*/
}

public class StopEvent
{
    [XmlElement("PreviousCall", typeof(PreviousCall))]
    [XmlElement("OnwardCall", typeof(OnwardCall))]
    [XmlElement("ThisCall", typeof(ThisCall))]
    public List<Call> Calls { get; set; }
    
    [XmlElement("Service")]
    public Service Service { get; set; }
    
    [XmlElement("Extension")]
    public Extension Extension { get; set; }
}

public class Call
{
    [XmlElement("CallAtStop")]
    public CallAtStop CallAtStop { get; set; }
    
    
}

public class PreviousCall : Call {}

public class ThisCall : Call {}

public class OnwardCall : Call {}

public class CallAtStop
{
    [XmlElement("Order")]
    public string Order { get; set; }
    
    [XmlElement("StopPointRef")]
    public string? StopPointRef { get; set; }
    
    [XmlElement("StopPointName")]
    public TextNode StopPointName { get; set; }
    
    [XmlElement("PlannedQuay")]
    public TextNode PlannedQuay { get; set; }
    
    [XmlElement("ServiceArrival")]
    public ServiceArrival? ServiceArrival { get; set; }
    
    [XmlElement("ServiceDeparture")]
    public ServiceDeparture? ServiceDeparture { get; set; }
}

public class ServiceArrival
{
    [XmlElement("TimetabledTime")]
    public string TimetabledTime { get; set; }
}

public class ServiceDeparture
{
    [XmlElement("TimetabledTime")]
    public string TimetabledTime { get; set; }
}

public class Service
{
    [XmlElement("OperatingDayRef")]
    public string OperatingDayRef { get; set; }
    
    [XmlElement("JourneyRef")]
    public string JourneyRef { get; set; }
    
    [XmlElement("LineRef", Namespace = "http://www.siri.org.uk/siri")]
    public string LineRef { get; set; }
    
    [XmlElement("DirectionRef", Namespace = "http://www.siri.org.uk/siri")]
    public string DirectionRef { get; set; }
    
    [XmlElement("Mode")]
    public Mode Mode { get; set; }
    
    [XmlElement("PublishedLineName")]
    public TextNode PublishedLineName { get; set; }
    
    [XmlElement("OperatorRef")]
    public string OperatorRef { get; set; }
    
    [XmlElement("Attribute")]
    public List<Attribute> Attribute { get; set; }
    
    [XmlElement("OriginStopPointRef")]
    public string OriginStopPointRef { get; set; }
    
    [XmlElement("OriginText")]
    public TextNode OriginText { get; set; }
    
    [XmlElement("DestinationStopPointRef")]
    public string DestinationStopPointRef { get; set; }
    
    [XmlElement("DestinationText")]
    public TextNode DestinationText { get; set; }
}

public class Extension
{
    [XmlElement("TransportTypeName")]
    public TextNode TransportTypeName { get; set; }
    
    [XmlElement("PublishedJourneyNumber")]
    public TextNode PublishedJourneyNumber { get; set; }
    
    [XmlElement("OperatorName")]
    public TextNode OperatorName { get; set; }
}

public class Mode
{
    [XmlElement("PtMode")]
    public string PtMode { get; set; }
    
    [XmlElement("RailSubmode", Namespace = "http://www.siri.org.uk/siri")]
    public string RailSubmode { get; set; }
    
    [XmlElement("Name")]
    public TextNode Name { get; set; }
    
    [XmlElement("ShortName")]
    public TextNode ShortName { get; set; }
}

public class Attribute
{
    [XmlElement("Text")]
    public TextNode Text { get; set; }
    
    [XmlElement("Code")]
    public string Code { get; set; }
    
    [XmlElement("AccessFacility", Namespace = "http://www.siri.org.uk/siri")]
    public string? AccessFacility { get; set; }
}

public class TextNode
{
    public string Text { get; set; }
}
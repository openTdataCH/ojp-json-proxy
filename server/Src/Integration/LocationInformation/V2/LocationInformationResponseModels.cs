using System.Xml.Serialization;

namespace TransportProxy.Application.LocationInformation.V2
{
    [XmlRoot("OJP", Namespace = "http://www.siri.org.uk/siri")]
    public class OjpLocationInformation
    {
        [XmlElement("OJPResponse")] public OjpResponse OJPResponse { get; set; }
    }

    public class OjpResponse
    {
        [XmlElement("ServiceDelivery")] public ServiceDelivery ServiceDelivery { get; set; }
    }

    public class ServiceDelivery
    {
        [XmlElement("ResponseTimestamp")] public string ResponseTimestamp { get; set; }

        [XmlElement("ProducerRef")] public string ProducerRef { get; set; }

        [XmlElement("ResponseMessageIdentifier")]
        public string ResponseMessageIdentifier { get; set; }

        [XmlElement("Status")] 
        public bool Status { get; set; }

        [XmlElement("OJPLocationInformationDelivery", Namespace = "http://www.vdv.de/ojp")]
        public OjpLocationInformationDelivery OJPLocationInformationDelivery { get; set; }
    }

    [XmlRoot("OJPLocationInformationDelivery", Namespace = "http://www.vdv.de/ojp")]
    public class OjpLocationInformationDelivery
    {
        [XmlElement("ResponseTimestamp", Namespace = "http://www.siri.org.uk/siri")]
        public string ResponseTimestamp { get; set; }

        [XmlElement("Status", Namespace = "http://www.siri.org.uk/siri")]
        public string Status { get; set; }

        [XmlElement("CalcTime")] 
        public string CalcTime { get; set; }

        [XmlElement("Location")] 
        public List<LocationWrapper> Locations { get; set; }
    }

    public class LocationWrapper
    {
        [XmlElement("Location")] 
        public Location Location { get; set; }
        
        [XmlElement("Complete")] 
        public bool Complete { get; set; }

        [XmlElement("Probability")] 
        public double Probability { get; set; }

        [XmlElement("Mode")] 
        public List<Mode> Modes { get; set; }
    }

    public class Location
    {
        [XmlElement("StopPlace")] 
        public StopPlace? StopPlace { get; set; }

        [XmlElement("LocationName")] 
        public TextNode LocationName { get; set; }

        [XmlElement("GeoPosition")] 
        public GeoPosition GeoPosition { get; set; }
    }

    public class Mode
    {
        [XmlElement("PtMode")] 
        public string PtMode { get; set; }

        [XmlElement("AirSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? AirSubmode { get; set; }
        
        [XmlElement("BusSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? BusSubmode { get; set; }
        
        [XmlElement("CoachSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? CoachSubmode { get; set; }
        
        [XmlElement("FunicularSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? FunicularSubmode { get; set; }
        
        [XmlElement("MetroSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? MetroSubmode { get; set; }
        
        [XmlElement("TramSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? TramSubmode { get; set; }
        
        [XmlElement("TelecabinSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? TelecabinSubmode { get; set; }
        
        [XmlElement("RailSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? RailSubmode { get; set; }
        
        [XmlElement("WaterSubmode", Namespace = "http://www.siri.org.uk/siri")] 
        public string? WaterSubmode { get; set; }
    }

    public class StopPlace
    {
        [XmlElement("StopPlaceRef")] 
        public int StopPlaceRef { get; set; }

        [XmlElement("StopPlaceName")] 
        public TextNode StopPlaceName { get; set; }
    }

    public class TextNode
    {
        public string Text { get; set; }
    }

    public class GeoPosition
    {
        [XmlElement("Longitude", Namespace = "http://www.siri.org.uk/siri")] 
        public double Longitude { get; set; }

        [XmlElement("Latitude", Namespace = "http://www.siri.org.uk/siri")] 
        public double Latitude { get; set; }
    }
}
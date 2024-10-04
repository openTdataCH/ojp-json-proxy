using System.Text;

namespace TransportProxy.Application.Common;

public static class Deserializer
{
    public static T DeserializeXml<T>(string xmlString)
    {
        using (var stringReader = new System.IO.StringReader(xmlString))
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }
    }
}
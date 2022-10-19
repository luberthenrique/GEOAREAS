using MongoDB.Bson.Serialization.Attributes;

namespace Geolocalizacao.Domain.Entities.SetoresCensitarios
{
    public class Geometry
    {
        [BsonElement("type")]
        public string Type { get; protected set; }
        
    }
}

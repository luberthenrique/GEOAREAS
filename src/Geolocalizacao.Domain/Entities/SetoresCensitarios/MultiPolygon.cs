using MongoDB.Bson.Serialization.Attributes;

namespace Geolocalizacao.Domain.Entities.SetoresCensitarios
{
    public class MultiPolygon : Geometry
    {
        public MultiPolygon(string type, double[][][][] coordinates)
        {
            Type = type;
            Coordinates = coordinates;
        }

        [BsonElement("coordinates")]
        public double[][][][] Coordinates { get; private set; }
    }
}

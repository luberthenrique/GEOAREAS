using System.Text.Json.Serialization;

namespace Geolocalizacao.Infra.CrossCutting.Nominatim.Models
{
    public class GeoJson
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("coordinates")]
        public object Coordinates { get; set; }
    }
}

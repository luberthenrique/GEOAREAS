using System.Text.Json.Serialization;

namespace Geolocalizacao.Service.Models
{
    public class Geolocalizacao
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("coordinates")]
        public object Coordenadas { get; set; }
    }
}

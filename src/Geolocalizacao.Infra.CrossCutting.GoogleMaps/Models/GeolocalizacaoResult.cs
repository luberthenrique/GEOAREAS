using System.Text.Json.Serialization;

namespace Geolocalizacao.Infra.CrossCutting.GoogleMaps.Models
{
    public partial class GeolocalizacaoResult
    {
        [JsonPropertyName("plus_code")]
        public PlusCode PlusCode { get; set; }
        [JsonPropertyName("results")]
        public Endereco[] Results { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("error_message")]
        public string Error { get; set; }
    }

    public partial class PlusCode
    {
        [JsonPropertyName("compound_code")]
        public string CompoundCode { get; set; }

        [JsonPropertyName("global_code")]
        public string GlobalCode { get; set; }
    }

    public partial class Endereco
    {
        [JsonPropertyName("address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("types")]
        public string[] Types { get; set; }
    }

    public partial class AddressComponent
    {
        [JsonPropertyName("long_name")]
        public string LongName { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("types")]
        public string[] Types { get; set; }
    }

    public partial class Geometry
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("viewport")]
        public Viewport Viewport { get; set; }

        [JsonPropertyName("bounds")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Bounds Bounds { get; set; }
    }

    public partial class Bounds
    {
        [JsonPropertyName("northeast")]
        public Location Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Location Southwest { get; set; }
    }

    public partial class Location
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public enum LocationType { Approximate, GeometricCenter, Rooftop };

    public partial class Viewport
    {
        [JsonPropertyName("northeast")]
        public Location Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Location Southwest { get; set; }
    }
}
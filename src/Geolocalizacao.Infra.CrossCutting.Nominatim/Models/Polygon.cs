using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Geolocalizacao.Infra.CrossCutting.Nominatim.Models
{
    public class Polygon
    {
        [JsonPropertyName("place_id")]
        public long IdPlace { get; set; }
        [JsonPropertyName("licence")]
        public string Licence { get; set; }
        [JsonPropertyName("osm_type")]
        public string OsmType { get; set; }
        [JsonPropertyName("osm_id")]
        public long OsmId { get; set; }
        [JsonPropertyName("boundingbox")]
        public string[] DoundingBox { get; set; }
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lon")]
        public string Lon { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
        [JsonPropertyName("class")]
        public string Class { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("importance")]
        public double Importance { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
        [JsonPropertyName("geojson")]
        public GeoJson Geojson { get; set; }

    }
}

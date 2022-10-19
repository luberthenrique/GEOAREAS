namespace Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Models
{
    public class Localizacao
    {
        public object Id { get; set; }
        public long IdPlace { get; set; }
        public string Licence { get; set; }
        public string OsmType { get; set; }
        public long OsmId { get; set; }
        public string[] DoundingBox { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string DisplayName { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public double Importance { get; set; }
        public string Icon { get; set; }
        public Geolocalizacao Geojson { get; set; }

        public string Area { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }
}

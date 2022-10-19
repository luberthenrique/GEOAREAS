namespace Geolocalizacao.Infra.CrossCutting.GoogleMaps.Models
{
    public class Address
    {
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string DistrictName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

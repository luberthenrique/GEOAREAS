namespace Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Models
{
    public class MongoConnection
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool IsSSL { get; set; }
    }
}

namespace Geolocalizacao.Domain.Core.Models
{
    public class NoSqlAppConfig
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool IsSSL { get; set; }
    }
}

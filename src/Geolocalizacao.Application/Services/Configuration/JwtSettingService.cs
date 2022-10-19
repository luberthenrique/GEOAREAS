namespace Geolocalizacao.Application.Services.Configuration
{
    public class JwtSettingService
    {
        public JwtSettingService(string secret, int expiration, string issuer, string validAt)
        {
            Secret = secret;
            Expiration = expiration;
            Issuer = issuer;
            ValidAt = validAt;
        }

        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string ValidAt { get; set; }
    }
}

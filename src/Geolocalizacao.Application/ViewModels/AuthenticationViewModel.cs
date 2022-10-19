using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class AuthenticationViewModel
    {
        [Display(Name = "API Key")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
        [Display(Name = "Secret Key")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }
    }
}

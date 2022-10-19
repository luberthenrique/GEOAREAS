using System;
using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class AuthenticationResultViewModel
    {
        [Display(Name = "API Key")]
        public string ApiKey { get; set; }
        [Display(Name = "Token")]
        public string Token { get; set; }

        public DateTime DataExpiracao { get; set; }
    }
}

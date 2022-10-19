using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um email válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Senha { get; set; }
        public bool LembrarMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}

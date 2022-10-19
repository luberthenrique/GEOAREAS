using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class UsuarioChangePasswordViewModel : UsuarioPasswordViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string SenhaAtual { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class UsuarioPasswordViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string ConfirmacaoSenha { get; set; }

        public string Token { get; set; }
    }
}

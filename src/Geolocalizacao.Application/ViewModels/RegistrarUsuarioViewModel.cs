using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class RegistrarUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 120, MinimumLength = 5, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido.")]
        [StringLength(maximumLength: 256, MinimumLength = 5, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Email { get; set; }
    }
}

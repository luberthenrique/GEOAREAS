using System;
using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class ApiClienteViewModel : BaseViewModel
    {
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid? IdCliente { get; set; }
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string Nome { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public Guid? IdUsuarioInclusao { get; set; }
        public DateTime? DataHoraInclusao { get; set; }
        public Guid? IdUsuarioAlteracao { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }

        public UsuarioViewModel UsuarioInclusao { get; set; }
    }
}

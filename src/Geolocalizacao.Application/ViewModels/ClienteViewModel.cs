using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Application.ViewModels
{
    public class ClienteViewModel : BaseViewModel
    {
        public ClienteViewModel()
        {            
        }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string Cnpj { get; set; }

        [Display(Name = "Inscrição Municipal")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 18, MinimumLength = 11, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string InscricaoMunicipal { get; set; }

        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 300, MinimumLength = 2, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string RazaoSocial { get; set; }
        
        [Display(Name = "Observação")]
        [MaxLength(300, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
        public string Observacao { get; set; }


        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 120, MinimumLength = 2, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Logradouro { get; set; }
        [Display(Name = "Numero")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Numero { get; set; }
        [Display(Name = "Complemento")]
        [MaxLength(60, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Complemento { get; set; }
        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Bairro { get; set; }
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Cidade { get; set; }
        [Display(Name = "UF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 2, MinimumLength = 2, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Uf { get; set; }
        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string Cep { get; set; }

        [Display(Name = "Telefone 1")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 11, MinimumLength = 10, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Telefone1 { get; set; }
        [Display(Name = "Telefone 2")]
        [StringLength(maximumLength: 11, ErrorMessage = "O campo {0} deve conter no máxixo {1} caracteres.")]
        public string Telefone2 { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 254, MinimumLength = 10, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string Email { get; set; }

        private ICollection<ApiClienteViewModel> Apis { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Geolocalizacao.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid? Id { get; set; }
        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid IdPerfil { get; set; }
        [Display(Name = "Gestor")]
        public Guid? IdUsuario { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength: 120, MinimumLength = 4, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }
        public bool Habilitado { get; set; }
        [Display(Name = "Email")]
        [StringLength(maximumLength: 120, MinimumLength = 4, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.")]
        public string Email { get; set; }
        public string Token { get; set; }

        public bool SenhaCadastrada { get; set; }

        public PerfilUsuarioViewModel PerfilUsuario { get; set; }
        public List<Claim> Claims { get; set; }
    }
}

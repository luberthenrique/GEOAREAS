using System;
using System.Collections.Generic;

namespace Geolocalizacao.Application.ViewModels
{
    public class PerfilUsuarioViewModel
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string NomeNormalizado { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsAdmin { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
    }
}

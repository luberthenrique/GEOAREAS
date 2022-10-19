using System;

namespace Geolocalizacao.Application.ViewModels
{
    public class ArquivoSetoresCensitariosViewModel : BaseViewModel
    {
        public int Status { get; set; }
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataInclusao { get; set; }
        public Guid IdUsuarioInclusao { get; set; }
    }
}

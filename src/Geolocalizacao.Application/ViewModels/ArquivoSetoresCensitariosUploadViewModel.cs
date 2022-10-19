using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Geolocalizacao.Application.ViewModels
{
    public class ArquivoSetoresCensitariosUploadViewModel
    {
        public string Nome { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}

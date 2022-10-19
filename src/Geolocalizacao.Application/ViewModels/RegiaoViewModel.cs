using System.Collections.Generic;

namespace Geolocalizacao.Application.ViewModels
{
    public class RegiaoViewModel : BaseViewModel
    {
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<double[]> Coordenadas { get; set; }
    }
}

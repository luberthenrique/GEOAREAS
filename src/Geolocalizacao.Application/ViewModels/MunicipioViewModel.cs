using System;
using System.Collections.Generic;

namespace Geolocalizacao.Application.ViewModels
{
    public class MunicipioViewModel : BaseViewModel
    {
        public long? PlaceId { get; set; }
        public string Tipo { get; set; }
        public string Regiao { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public string TipoPoligono { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataAtualizacaoBairros { get; set; }
        public List<double[]> Coordenadas { get; set; }
    }
}

using Geolocalizacao.Application.Services;
using Newtonsoft.Json;

namespace Geolocalizacao.Application.ViewModels
{
    public class SetorViewModel : BaseViewModel
    {
        [JsonProperty("feature_id")]
        public long FeatureId { get; set; }
        [JsonProperty("geometry")]
        public GeometryViewModel Geometry { get; set; }
        [JsonProperty("codigo")]
        public string Codigo { get; set; }
        [JsonProperty("codigo_situacao")]
        public string CodigoSituacao { get; set; }
        [JsonProperty("situacao")]
        public string Situacao { get; set; }
        [JsonProperty("codigo_uf")]
        public string CodigoUf { get; set; }
        [JsonProperty("nome_uf")]
        public string NomeUf { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("codigo_municipio")]
        public string CodigoMunicipio { get; set; }
        [JsonProperty("municipio")]
        public string Municipio { get; set; }
        [JsonProperty("codigo_distrito")]
        public string CodigoDistrito { get; set; }
        [JsonProperty("nome_distrito")]
        public string NomeDistrito { get; set; }
        [JsonProperty("codigo_subdistrito")]
        public string CodigoSubDistrito { get; set; }
        [JsonProperty("nome_subdistrito")]
        public string NomeSubDistrito { get; set; }
    }
}

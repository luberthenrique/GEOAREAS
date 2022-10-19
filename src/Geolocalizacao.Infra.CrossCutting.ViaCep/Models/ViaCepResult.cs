using System.Text.Json.Serialization;

namespace Geolocalizacao.Infra.CrossCutting.ViaCep.Models
{
    /// <summary>
    /// The Via CEP result class.
    /// </summary>
    public sealed class ViaCepResult
    {
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [JsonPropertyName("cep")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>
        /// The street.
        /// </value>
        [JsonPropertyName("logradouro")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the complement.
        /// </summary>
        /// <value>
        /// The complement.
        /// </value>
        [JsonPropertyName("complemento")]
        public string Complement { get; set; }

        /// <summary>
        /// Gets or sets the neighborhood.
        /// </summary>
        /// <value>
        /// The neighborhood.
        /// </value>
        [JsonPropertyName("bairro")]
        public string Neighborhood { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonPropertyName("localidade")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state initials.
        /// </summary>
        /// <value>
        /// The state initials.
        /// </value>
        [JsonPropertyName("uf")]
        public string StateInitials { get; set; }

        /// <summary>
        /// Gets or sets the ibge code.
        /// </summary>
        /// <value>
        /// The ibge code.
        /// </value>
        [JsonPropertyName("ibge")]
        public string IBGECode { get; set; }

        /// <summary>
        /// Gets or sets the gia code.
        /// </summary>
        /// <value>
        /// The gia code.
        /// </value>
        [JsonPropertyName("gia")]
        public string GIACode { get; set; }

        /// <summary>
        /// Gets or sets the gia code.
        /// </summary>
        /// <value>
        /// The gia code.
        /// </value>
        [JsonPropertyName("ddd")]
        public string DDD { get; set; }

        /// <summary>
        /// Gets or sets the gia code.
        /// </summary>
        /// <value>
        /// The gia code.
        /// </value>
        [JsonPropertyName("siafi")]
        public string Siafi { get; set; }

        [JsonPropertyName("erro")]
        public bool Erro { get; set; }
    }
}

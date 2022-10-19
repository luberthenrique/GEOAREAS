using Newtonsoft.Json;
using System;

namespace Geolocalizacao.Application.ViewModels
{
    public class BaseViewModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
    }
}

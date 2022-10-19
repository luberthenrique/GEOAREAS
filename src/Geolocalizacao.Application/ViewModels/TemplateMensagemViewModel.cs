using System;

namespace Geolocalizacao.Application.ViewModels
{
    public class TemplateMensagemViewModel
    {
        public Guid Id { get; set; }
        public int Tipo { get; set; }
        public int? Etapa { get; set; }
        public string Texto { get; set; }
        public bool Ativo { get; set; }
    }
}

using Geolocalizacao.Domain.Core.Commands;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Domain.Commands.SetoresCensitarios
{
    public abstract class SetoresCensitarioCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public List<IFormFile> Files { get; protected set; }
        public List<Setor> Setores { get; protected set; }
    }
}

using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Domain.Commands.SetoresCensitarios
{
    public class CarregarSetoresCommand : SetoresCensitarioCommand
    {
        public CarregarSetoresCommand(Guid id, List<Setor> setores)
        {
            Id = id;
            Setores = setores;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}

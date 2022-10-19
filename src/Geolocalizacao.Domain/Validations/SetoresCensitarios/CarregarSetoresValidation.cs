using FluentValidation;
using Geolocalizacao.Domain.Commands.SetoresCensitarios;
using System.IO;
using System.Linq;

namespace Geolocalizacao.Domain.Validations.SetoresCensitarios
{
    public class CarregarSetoresValidation : SetoresCensitariosValidation<SetoresCensitarioCommand>
    {
        public CarregarSetoresValidation()
        {
            RuleFor(c => c.Setores)
                .NotNull()
                .NotEmpty()
                .WithMessage($"O carregamento de setores é obrigatorio.");

            RuleFor(c => c.Setores)
                .Must(c => c.Select(f => f.Uf).Distinct().Count() <= 1)
                    .WithMessage("É permitido o carregamento de setores de somente um estado.");
        }
    }
}

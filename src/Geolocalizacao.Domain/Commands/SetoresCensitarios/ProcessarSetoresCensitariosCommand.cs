using Geolocalizacao.Domain.Validations.SetoresCensitarios;
using System;

namespace Geolocalizacao.Domain.Commands.SetoresCensitarios
{
    public class ProcessarSetoresCensitariosCommand : SetoresCensitarioCommand
    {
        public ProcessarSetoresCensitariosCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProcessarSetoresCensitariosValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

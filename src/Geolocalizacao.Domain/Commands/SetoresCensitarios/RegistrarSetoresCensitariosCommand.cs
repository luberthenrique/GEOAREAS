using Geolocalizacao.Domain.Core.Commands;
using Geolocalizacao.Domain.Validations.SetoresCensitarios;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Domain.Commands.SetoresCensitarios
{
    public class RegistrarSetoresCensitariosCommand : SetoresCensitarioCommand
    {
        public RegistrarSetoresCensitariosCommand(
            string nome, 
            List<IFormFile> files)
        {
            Nome = nome;
            Files = files;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarSetoresCensitariosValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

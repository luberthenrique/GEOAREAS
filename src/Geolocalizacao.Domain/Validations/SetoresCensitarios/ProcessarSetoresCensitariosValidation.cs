using Geolocalizacao.Domain.Commands.SetoresCensitarios;

namespace Geolocalizacao.Domain.Validations.SetoresCensitarios
{
    public class ProcessarSetoresCensitariosValidation : SetoresCensitariosValidation<ProcessarSetoresCensitariosCommand>
    {
        public ProcessarSetoresCensitariosValidation()
        {
            ValidarId();
        }
    }
}

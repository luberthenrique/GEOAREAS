using Geolocalizacao.Domain.Commands.SetoresCensitarios;

namespace Geolocalizacao.Domain.Validations.SetoresCensitarios
{
    public class RegistrarSetoresCensitariosValidation : SetoresCensitariosValidation<RegistrarSetoresCensitariosCommand>
    {
        public RegistrarSetoresCensitariosValidation()
        {
            ValidarNome();
            ValidarArquivos();
        }
    }
}

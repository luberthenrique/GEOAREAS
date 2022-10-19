using System.Text.RegularExpressions;

namespace Geolocalizacao.Domain.Validations.Assistants
{
    public static class CepValidation
    {
        public static bool Validar(string cep)
        {
            if (string.IsNullOrEmpty(cep))
                return false;
            else if (!Regex.IsMatch(cep, @"^\d{8}$"))
                return false;

            return true;
        }
    }
}

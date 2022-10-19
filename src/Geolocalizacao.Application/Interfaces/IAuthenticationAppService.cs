using Geolocalizacao.Application.ViewModels;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Interfaces
{
    public interface IAuthenticationAppService
    {
        Task<AuthenticationResultViewModel> Autenticar(AuthenticationViewModel authenticationViewModel);
    }
}

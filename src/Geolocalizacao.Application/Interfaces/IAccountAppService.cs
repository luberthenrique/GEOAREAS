using Geolocalizacao.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<UsuarioViewModel> Login(LoginViewModel loginViewModel);
        Task EfetuarLogout();
        Task<UsuarioViewModel> ObterUsuario(string email);
        Task<List<Claim>> ObterClaims(Guid idPerfil);
        Task ResendConfirmation(UsuarioEmailViewModel usuarioViewModel);
        Task ChangePasswordAsync(UsuarioChangePasswordViewModel usuarioAlteracaoSenhaViewModel);
        Task AddPasswordAsync(UsuarioPasswordViewModel usuarioAlteracaoSenhaViewModel);
        Task GeneratePasswordResetTokenAsync(UsuarioEmailViewModel usuarioViewModel);
        Task ResetPasswordConfirm(UsuarioPasswordViewModel usuarioViewModel);
        Task<UsuarioViewModel> ConfirmEmailAsync(ConfirmarEmailViewModel confirmarEmailViewModel);
    }
}

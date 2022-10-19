using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.Services.Configuration;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Geolocalizacao.Application.Services
{
    public class AccountAppService : ApplicationBaseService, IAccountAppService
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<PerfilUsuario> _roleManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IEmailSender _emailSenderService;
        private readonly JwtSettingService _jwtSettingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountAppService(
            SignInManager<Usuario> signInManager,
            RoleManager<PerfilUsuario> roleManager,
            UserManager<Usuario> userManager,
            IUsuarioRepository usuarioRepository,
            IPerfilUsuarioRepository perfilUsuarioRepository,
            IEmailSender emailSenderService,
            JwtSettingService jwtSettingService,
            IHttpContextAccessor httpContextAccessor,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _emailSenderService = emailSenderService;

            _jwtSettingService = jwtSettingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UsuarioViewModel> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Senha, loginViewModel.LembrarMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    _notifications.AddNotification("", "A conta está bloqueada, entre em contato com o administrador.");
                else if (result.IsNotAllowed)
                    _notifications.AddNotification("", "Acesso não permitido, entre em contato com o administrador.");
                else
                    _notifications.AddNotification("", "Não foi possível efetuar login com as credenciais informadas.");

                return null;
            }

            var usuario = await _usuarioRepository.GetByEmail(loginViewModel.Email);
            var perfilUsuario = await _roleManager.FindByIdAsync(usuario.IdPerfil.ToString());

            var usuarioViewModel = new UsuarioViewModel 
            { 
                Id = usuario.Id,
                IdPerfil = usuario.IdPerfil,
                IdUsuario = usuario.IdUsuario,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Habilitado = usuario.Habilitado
            };

            var claims = (await _roleManager.GetClaimsAsync(perfilUsuario)).ToList();

            usuarioViewModel.Token = GenerateJwt(usuario, claims, perfilUsuario.IsAdmin);
            usuarioViewModel.Claims = claims;
            usuarioViewModel.PerfilUsuario = new PerfilUsuarioViewModel { 
                Id = perfilUsuario.Id,
                Nome = perfilUsuario.Name,
                IsAdmin = perfilUsuario.IsAdmin,
                NomeNormalizado = perfilUsuario.NormalizedName,
                ConcurrencyStamp = perfilUsuario.ConcurrencyStamp
            };

            return usuarioViewModel;
        }

        private string GenerateJwt(Usuario usuario, List<Claim> userClaims, bool isAdmin)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }

            claims.AddRange(userClaims);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettingService.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                //Issuer = _appSettings.Issuer,
                //Audience = _appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(_jwtSettingService.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public async Task ResendConfirmation(UsuarioEmailViewModel usuarioViewModel)
        {

            var usuario = await _userManager.FindByEmailAsync(usuarioViewModel.Email);

            if (usuario == null)
            {
                _notifications.AddNotification("", "Email não registrado.");
                return;
            }

            if (usuario.EmailConfirmed)
            {
                _notifications.AddNotification("", "O email já foi confirmado.");
                return;
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            string confirmationToken = _userManager.
                 GenerateEmailConfirmationTokenAsync(usuario).Result;

            var request = _httpContextAccessor.HttpContext.Request;
            var host = $"{request.Scheme}://{request.Host.Value.ToString()}{request.PathBase.Value.ToString()}";

            string confirmationLink = $"{host}/auth/confirmemail?email={usuario.Email}&token={confirmationToken}";

            //Carregar layout de envio de email
            var directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "emailLayout", "confirmacaoEmail.html");
            string layout = System.IO.File.ReadAllText(@directory);
            layout = layout.Replace("{nome}", usuario.Nome);
            layout = layout.Replace("{linkConfirmacao}", confirmationLink);

            _ = _emailSenderService.SendEmailAsync(usuario.Email, "Confirme seu email", layout);

            scope.Complete();
        }

        public async Task ChangePasswordAsync(UsuarioChangePasswordViewModel usuarioAlteracaoSenhaViewModel)
        {
            if (usuarioAlteracaoSenhaViewModel.NovaSenha != usuarioAlteracaoSenhaViewModel.ConfirmacaoSenha)
            {
                _notifications.AddNotification("", "As senhas digitadas não coincidem.");
                return;
            }

            var usuario = await _userManager.FindByIdAsync(usuarioAlteracaoSenhaViewModel.Id.GetValueOrDefault().ToString());

            if (usuario == null)
                return;
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var retorno = await _userManager.ChangePasswordAsync(usuario, usuarioAlteracaoSenhaViewModel.SenhaAtual, usuarioAlteracaoSenhaViewModel.NovaSenha);
            if (!retorno.Succeeded)
            {
                foreach (var item in retorno.Errors)
                {
                    if (item.Code == "PasswordMismatch")
                    {
                        _notifications.AddNotification("", "A senha atual está incorreta.");
                    }
                    else
                    {
                        _notifications.AddNotification("", item.Description);
                    }
                }
                return;
            }

            _ = EnviarEmailAlteracaoSenha(usuario.Nome, usuario.Email);

            scope.Complete();
        }

        public async Task AddPasswordAsync(UsuarioPasswordViewModel usuarioAlteracaoSenhaViewModel)
        {
            if (usuarioAlteracaoSenhaViewModel.NovaSenha != usuarioAlteracaoSenhaViewModel.ConfirmacaoSenha)
            {
                _notifications.AddNotification("", "As senhas digitadas não coincidem.");
                return;
            }

            var usuario = await _userManager.FindByIdAsync(usuarioAlteracaoSenhaViewModel.Id.GetValueOrDefault().ToString());

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var retorno = await _userManager.AddPasswordAsync(usuario, usuarioAlteracaoSenhaViewModel.NovaSenha);
            if (!retorno.Succeeded)
            {
                foreach (var item in retorno.Errors)
                {
                    if (item.Code == "UserAlreadyHasPassword")
                    {
                        _notifications.AddNotification("", "O usuário já possui uma senha cadastrada.");
                    }
                    else
                    {
                        _notifications.AddNotification("", item.Description);
                    }


                }
                //TODO - notificar mensagem de erro


                return;
            }

            _ = EnviarEmailAlteracaoSenha(usuario.Nome, usuario.Email);
            scope.Complete();
        }

        public async Task GeneratePasswordResetTokenAsync(UsuarioEmailViewModel usuarioViewModel)
        {
            var usuario = await _userManager.FindByEmailAsync(usuarioViewModel.Email);

            var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var request = _httpContextAccessor.HttpContext.Request;
            var host = $"{request.Scheme}://{request.Host.Value.ToString()}{request.PathBase.Value.ToString()}";

            string confirmationLink = $"{host}/auth/resetar-senha-confirmar?id={usuario.Id}&token={passwordToken}";

            //Carregar layout de envio de email
            var directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "emailLayout", "redefinirSenha.html");
            string layout = System.IO.File.ReadAllText(@directory);
            layout = layout.Replace("{nome}", usuario.Nome);
            layout = layout.Replace("{linkRedefinir}", confirmationLink);

            _ = _emailSenderService.SendEmailAsync(usuario.Email, "Redefinição de senha", layout);

        }

        public async Task ResetPasswordConfirm(UsuarioPasswordViewModel usuarioViewModel)
        {
            if (usuarioViewModel.NovaSenha != usuarioViewModel.ConfirmacaoSenha)
            {
                _notifications.AddNotification("", "As senhas digitadas não coincidem.");
                return;
            }

            var usuario = await _userManager.FindByIdAsync(usuarioViewModel.Id.GetValueOrDefault().ToString());

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var retorno = await _userManager.ResetPasswordAsync(usuario, usuarioViewModel.Token.Replace(" ", "+"), usuarioViewModel.NovaSenha);
            if (!retorno.Succeeded)
            {
                foreach (var item in retorno.Errors)
                {
                    if (item.Code == "InvalidToken")
                    {
                        _notifications.AddNotification("", "Token inválido, favor solicitar novamente o reset de senha.");
                    }
                    else
                    {
                        _notifications.AddNotification("", item.Description);
                    }
                }
                return;
            }

            _ = EnviarEmailAlteracaoSenha(usuario.Nome, usuario.Email);

            scope.Complete();
        }

        public async Task<UsuarioViewModel> ConfirmEmailAsync(ConfirmarEmailViewModel confirmarEmailViewModel)
        {
            var usuario = await _userManager.FindByEmailAsync(confirmarEmailViewModel.Email);
            if (usuario == null || usuario.EmailConfirmed)
                return null;

            var result = await _userManager.ConfirmEmailAsync(usuario, confirmarEmailViewModel.Token.Replace(" ", "+"));

            if (!result.Succeeded)
            {
                //TODO - notificar mensagem de erro
                _notifications.HandleAll(result.Errors.Select(c => new DomainNotification("", c.Description)));
            }

            return new UsuarioViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task EfetuarLogout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UsuarioViewModel> ObterUsuario(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            return new UsuarioViewModel
            {
                Email = usuario.Email,
                Id = usuario.Id,
                IdPerfil = usuario.IdPerfil,
                Nome = usuario.Nome
            };
        }

        public async Task<List<Claim>> ObterClaims(Guid idPerfil)
        {
            var claims = await _roleManager.GetClaimsAsync(await _roleManager.FindByIdAsync(idPerfil.ToString()));

            return claims.ToList();
        }

        public async Task EnviarEmailAlteracaoSenha(string nome, string email)
        {
            //Carregar layout de envio de email
            var directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "emailLayout", "senhaAlterada.html");
            string layout = System.IO.File.ReadAllText(@directory);
            layout = layout.Replace("{nome}", nome);

            await _emailSenderService.SendEmailAsync(email, "Senha alterada", layout);
        }
    }
}

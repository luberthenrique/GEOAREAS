using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.Services.Configuration;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
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
    public class AuthenticationAppService : ApplicationBaseService, IAuthenticationAppService
    {
        private readonly IApiClienteRepository _apiClienteRepository;
        private readonly JwtSettingService _jwtSettingService;

        public AuthenticationAppService(
            IApiClienteRepository apiClienteRepository,
            JwtSettingService jwtSettingService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _apiClienteRepository = apiClienteRepository;

            _jwtSettingService = jwtSettingService;
        }

        public async Task<AuthenticationResultViewModel> Autenticar(AuthenticationViewModel authenticationViewModel)
        {
            var result = await _apiClienteRepository.GetAll()
                .FirstOrDefaultAsync(c => 
                    c.ApiKey == authenticationViewModel.ApiKey && 
                    c.SecretKey == authenticationViewModel.SecretKey
                );

            if (result is null)
            {
                _notifications.AddNotification("", "Não foi possível autenticar login com as credenciais informadas.");

                return null;
            }

            var claims = new List<Claim>
            {
                new Claim("Setor", "Get")
            };

            return GenerateJwt(result, claims);
        }

        private AuthenticationResultViewModel GenerateJwt(ApiCliente api, List<Claim> apiClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, api.ApiKey),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, "client"));

            claims.AddRange(apiClaims);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettingService.Secret);
            var dataExpiracao = DateTime.UtcNow.AddHours(_jwtSettingService.Expiration);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                //Issuer = _appSettings.Issuer,
                //Audience = _appSettings.ValidAt,
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new AuthenticationResultViewModel 
            { 
                ApiKey = api.ApiKey,
                Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                DataExpiracao = dataExpiracao
            };
        }
    }
}

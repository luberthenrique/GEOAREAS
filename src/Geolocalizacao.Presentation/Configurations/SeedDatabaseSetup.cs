using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Infra.Data.Context;
using Geolocalizacao.Presentation.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geolocalizacao.Presentation.Configurations
{
    public class SeedDatabaseSetup
    {
        private readonly IPerfilUsuarioAppService _perfilUsuarioAppService;
        private readonly IUsuarioAppService _usuarioAppService;
        private ILogger<Program> _logger;

        public SeedDatabaseSetup(
            IPerfilUsuarioAppService perfilUsuarioAppService,
            IUsuarioAppService usuarioAppService,
            ILogger<Program> logger)
        {
            _perfilUsuarioAppService = perfilUsuarioAppService;
            _usuarioAppService = usuarioAppService;
            _logger = logger;
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            //Aplicar migrations
            serviceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            serviceProvider.GetRequiredService<NoSqlDbContext>().Initialize();
            _logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            var menuService = serviceProvider.GetService<MenuHelper>();
            var claims = menuService.GetClaims();

            SeedRoles(claims);

            var usuario = _usuarioAppService.GetByEmailAsync("teste@teste.com").GetAwaiter().GetResult();
            var perfilAdmin = _perfilUsuarioAppService.GetByNameAsync(UserRoles.Admin).GetAwaiter().GetResult();
            if (usuario == null)
            {
                var user = new UsuarioViewModel
                {
                    Id = Guid.NewGuid(),
                    IdPerfil = perfilAdmin.Id.GetValueOrDefault(),
                    Nome = "USUÁRIO SUPORTE",
                    Email = "teste@teste.com",
                    Habilitado = true
                };

                _usuarioAppService.RegisterConfirmedUserAsync(user, "123456", UserRoles.Admin).GetAwaiter().GetResult();
            }

            //SeedMunicipiosEBairros();

        }

        private void SeedRoles(IEnumerable<ClaimViewModel> claims)
        {
            SeedPerfilUsuario(
                "SUPORTE",
                claims,
                true);

            SeedPerfilUsuario(
                "ADMINISTRADOR",
                claims,
                true);
        }

        private void SeedPerfilUsuario(string nome, IEnumerable<ClaimViewModel> permissoes, bool isAdmin)
        {
            var tempRole = new PerfilUsuarioViewModel
            {
                Id = Guid.NewGuid(),
                IsAdmin = isAdmin,
                Nome = nome,
                Claims = permissoes.ToList()
            };

            if (!_perfilUsuarioAppService.AnyAsync(nome).GetAwaiter().GetResult())
            {
                _perfilUsuarioAppService.RegisterAsync(tempRole).GetAwaiter().GetResult();
            }
            else
            {
                _perfilUsuarioAppService.RegisterNewClaims(tempRole).GetAwaiter().GetResult();
            }
        }

    }
}

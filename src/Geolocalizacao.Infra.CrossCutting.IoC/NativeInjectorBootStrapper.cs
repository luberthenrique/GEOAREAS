using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.Services;
using Geolocalizacao.Domain.CommandHandler;
using Geolocalizacao.Domain.Commands.Clientes;
using Geolocalizacao.Domain.Commands.SetoresCensitarios;
using Geolocalizacao.Domain.Core.Bus;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using Geolocalizacao.Infra.CrossCutting.Bus;
using Geolocalizacao.Infra.CrossCutting.Identity.Models;
using Geolocalizacao.Infra.CrossCutting.ViaCep.Client;
using Geolocalizacao.Infra.Data.Repository;
using Geolocalizacao.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Geolocalizacao.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Application            

            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IApiClienteAppService, ApiClienteAppService>();
            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();
            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IPerfilUsuarioAppService, PerfilUsuarioAppService>();
            services.AddTransient<ISetorAppService, SetorAppService>();
            services.AddTransient<ISetoresCensitariosAppService, SetoresCensitariosAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();

            #endregion

            #region Domain BUS
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            #endregion

            #region Domain

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #endregion

            #region Domain Commands           
            // ApiCliente
            services.AddScoped<IRequestHandler<RegistrarApiClienteCommand, bool>, ApiClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverApiClienteCommand, bool>, ApiClienteCommandHandler>();

            // Cliente
            services.AddScoped<IRequestHandler<AlterarClienteCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverClienteCommand, bool>, ClienteCommandHandler>();

            // Setores Censitários
            services.AddScoped<IRequestHandler<CarregarSetoresCommand, bool>, SetoresCensitariosCommandHandler>();
            services.AddScoped<IRequestHandler<ProcessarSetoresCensitariosCommand, bool>, SetoresCensitariosCommandHandler>();
            services.AddScoped<IRequestHandler<RegistrarSetoresCensitariosCommand, bool>, SetoresCensitariosCommandHandler>();            
            #endregion

            #region Infra - Data

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IApiClienteRepository, ApiClienteRepository>();
            services.AddScoped<ISetorRepository, SetorRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddScoped<IArquivoSetoresCensitariosRepository, ArquivoSetoresCensitariosRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            #endregion

            #region Infra - Identity

            services.AddScoped<IUser, AspNetUser>();

            #endregion

            #region CrossCutting

            #region ViaCep

            services.AddScoped<IViaCepClient, ViaCepClient>();

            #endregion

            #endregion
        }

        public static void RegisterServicesApi(IServiceCollection services)
        {
            #region Application            

            services.AddScoped<IApiClienteAppService, ApiClienteAppService>();
            services.AddScoped<IAuthenticationAppService, AuthenticationAppService>();
            services.AddTransient<ISetorAppService, SetorAppService>();

            #endregion

            #region Domain BUS
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            #endregion

            #region Domain

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #endregion

            #region Domain Commands           
            #endregion

            #region Infra - Data

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IApiClienteRepository, ApiClienteRepository>();
            services.AddScoped<ISetorRepository, SetorRepository>();

            #endregion

            #region Infra - Identity

            services.AddScoped<IUser, AspNetUser>();

            #endregion

            #region CrossCutting

            #region ViaCep

            services.AddScoped<IViaCepClient, ViaCepClient>();

            #endregion

            #endregion
        }
    }
}

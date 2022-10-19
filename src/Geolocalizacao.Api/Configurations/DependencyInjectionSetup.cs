using Geolocalizacao.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Geolocalizacao.Api.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServicesApi(services);

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}
using Geolocalizacao.Infra.CrossCutting.Email.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Geolocalizacao.Presentation.Configurations
{
    public static class EmailSenderSetup
    {
        public static void AddEmailSenderSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailSender, EmailSenderService>(i =>
                new EmailSenderService(
                    configuration["EmailSender:DisplayName"],
                    configuration["EmailSender:From"],
                    configuration["EmailSender:Host"],
                    configuration.GetValue<int>("EmailSender:Port"),
                    configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    configuration["EmailSender:UserName"],
                    configuration["EmailSender:Password"]
                )
            );
        }
    }
}

using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.Email.Services
{
    public class EmailSenderService : IEmailSender
    {
        public string _displayName { get; set; }
        private string _from;
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _userName;
        private string _password;

        // Get our parameterized configuration
        public EmailSenderService(string displayName, string from, string host, int port, bool enableSSL, string userName, string password)
        {
            _displayName = displayName;
            _from = from;
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _userName = userName;
            _password = password;
        }

        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL
            };
            var message = new MailMessage(
                new MailAddress(_from, _displayName), 
                new MailAddress(email)) 
            { 
                IsBodyHtml = true, 
                Subject = subject, Body = htmlMessage
            };

            return client.SendMailAsync(message);
        }
    }
}

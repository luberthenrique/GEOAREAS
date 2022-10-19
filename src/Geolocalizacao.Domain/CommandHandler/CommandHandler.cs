using Geolocalizacao.Domain.Core.Commands;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.CommandHandler
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _notifications.AddNotification(message.MessageType, error.ErrorMessage);
            }
        }

        protected void AddNotification(string type, string message)
        {
            _notifications.AddNotification(type, message);
        }

        protected bool HasNotifications()
        {
            return _notifications.HasNotifications();
        }

        public async Task<bool> Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (await _uow.Commit()) return true;

           // _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}

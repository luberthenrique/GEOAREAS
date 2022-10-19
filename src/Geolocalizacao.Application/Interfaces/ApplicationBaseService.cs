using AutoMapper;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;

namespace Geolocalizacao.Application.Interfaces
{
    public abstract class ApplicationBaseService
    {
        protected readonly DomainNotificationHandler _notifications;

        protected ApplicationBaseService(
            INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }
    }
}

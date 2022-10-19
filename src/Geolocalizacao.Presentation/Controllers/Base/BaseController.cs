using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Geolocalizacao.Presentation.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;

        protected BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = _notifications.GetNotifications();
            foreach (var erro in erros)
            {
                NotifyError(erro.Key, erro.Value);
            }
        }

        protected void NotifyError(string code, string message)
        {
            ModelState.AddModelError(code, message);
        }

        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }

        protected bool HasNotifications
        {
            get
            {
                NotifyModelStateErrors();
                return !ModelState.IsValid;
            }
        }
    }
}

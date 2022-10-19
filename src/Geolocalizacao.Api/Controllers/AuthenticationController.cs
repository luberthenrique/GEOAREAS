using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Geolocalizacao.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationAppService _authenticationAppService;
        public AuthenticationController(
            IAuthenticationAppService authenticationAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _authenticationAppService = authenticationAppService;
        }

        [HttpPost]
        [Route("api/auth")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResultViewModel>> Login([FromBody] AuthenticationViewModel authenticationViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _authenticationAppService.Autenticar(authenticationViewModel);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}

using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Geolocalizacao.Presentation.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountAppService _accountAppService;
        private readonly IUsuarioAppService _usuarioAppService;
        public AccountController(
            IAccountAppService accountAppService,
            IUsuarioAppService usuarioAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _accountAppService = accountAppService;
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        [Route("api/identity/login")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioViewModel>> Login([FromBody] LoginViewModel loginViewModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            loginViewModel.ReturnUrl = returnUrl;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var usuario = await _accountAppService.Login(loginViewModel);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [Route("api/identity/logout")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _accountAppService.EfetuarLogout();

            return Ok();
        }

        [HttpPost]
        [Route("api/identity/resend/confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendConfirmation([FromBody] UsuarioEmailViewModel usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _accountAppService.ResendConfirmation(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPost]
        [Route("api/identity/confirm/email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmarEmailViewModel confirmarEmailViewModel)
        {
            var usuario = await _accountAppService.ConfirmEmailAsync(confirmarEmailViewModel);

            if (usuario == null)
                return NotFound();

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPut("api/identity/password/add/{id}")]
        [Authorize]
        public async Task<IActionResult> AddPassword(Guid id, UsuarioPasswordViewModel usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            if (!await _usuarioAppService.AnyAsync(usuario.Id.GetValueOrDefault()))
            {
                return NotFound(usuario.Id);
            }

            await _accountAppService.AddPasswordAsync(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPut("api/identity/password/chang/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(Guid id, UsuarioChangePasswordViewModel usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            if (!await _usuarioAppService.AnyAsync(usuario.Id.GetValueOrDefault()))
            {
                return NotFound(usuario.Id);
            }

            await _accountAppService.ChangePasswordAsync(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPost("api/identity/password/reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] UsuarioEmailViewModel usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _accountAppService.GeneratePasswordResetTokenAsync(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPut("api/identity/password/reset/confirm/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordConfirm(Guid id, UsuarioPasswordViewModel usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            if (!await _usuarioAppService.AnyAsync(usuario.Id.GetValueOrDefault()))
            {
                return NotFound(usuario.Id);
            }

            await _accountAppService.ResetPasswordConfirm(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }
    }
}

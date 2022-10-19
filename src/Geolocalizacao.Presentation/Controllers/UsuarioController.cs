using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Presentation.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(
            IUsuarioAppService usuarioAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpGet("api/usuario")]
        //TODO - [Authorize(Policy = "GetUsuario")]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> GetUsuario()
        {
            return Ok(await _usuarioAppService.GetAllAsync());
        }

        [HttpGet("api/usuario/{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioViewModel>> GetUsuario(Guid id)
        {
            var usuario = await _usuarioAppService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPut("api/usuario/{id}")]
        [Authorize(Policy = "PutUsuario")]
        public async Task<IActionResult> PutUsuario(Guid id, UsuarioViewModel usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            if (!await _usuarioAppService.AnyAsync(usuario.Id.GetValueOrDefault()))
            {
                return NotFound(usuario.Id);
            }

            await _usuarioAppService.UpdateAsync(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPut("api/usuario/data/{id}")]
        [Authorize]
        public async Task<IActionResult> PutDataUsuario(Guid id, UsuarioDataViewModel usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            if (!await _usuarioAppService.AnyAsync(usuario.Id.GetValueOrDefault()))
            {
                return NotFound(usuario.Id);
            }

            await _usuarioAppService.UpdateDataAsync(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpPost("api/usuario")]
        [Authorize(Policy = "PostUsuario")]
        public async Task<IActionResult> PostUsuario(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usuarioAppService.RegisterAsync(usuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(usuario);
        }

        [HttpDelete("api/usuario/{id}")]
        [Authorize(Policy = "DeleteUsuario")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            if (!await _usuarioAppService.AnyAsync(id))
            {
                return NotFound();
            }

            await _usuarioAppService.RemoveAsync(id);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}

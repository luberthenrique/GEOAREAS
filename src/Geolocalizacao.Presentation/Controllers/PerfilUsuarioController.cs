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
    public class PerfilUsuarioController : BaseController
    {
        private readonly IPerfilUsuarioAppService _perfilUsuarioAppService;

        public PerfilUsuarioController(            
            IPerfilUsuarioAppService perfilUsuarioAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _perfilUsuarioAppService = perfilUsuarioAppService;
        }

        [HttpGet("api/perfilusuario")]
        //[Authorize(Policy = "GetPerfilUsuario")]
        public async Task<ActionResult<IEnumerable<PerfilUsuarioViewModel>>> GetPerfilUsuario()
        {
            return Ok(await _perfilUsuarioAppService.GetAllAsync());
        }

        [HttpGet("api/perfilusuario/{id}")]
        [Authorize(Policy = "GetPerfilUsuario")]
        public async Task<ActionResult<PerfilUsuarioViewModel>> GetPerfilUsuario(Guid id)
        {
            var perfilUsuario = await _perfilUsuarioAppService.GetByIdAsync(id);

            if (perfilUsuario == null)
            {
                return NotFound();
            }

            return perfilUsuario;
        }

        // PUT: api/PerfilUsuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/perfilusuario/{id}")]
        [Authorize(Policy = "PutPerfilUsuario")]
        public async Task<IActionResult> PutPerfilUsuario(Guid id, PerfilUsuarioViewModel perfilUsuario)
        {
            if (id != perfilUsuario.Id)
                return BadRequest();

            if (!await _perfilUsuarioAppService.AnyAsync(perfilUsuario.Id.GetValueOrDefault()))
            {
                return NotFound(perfilUsuario.Id);
            }

            await _perfilUsuarioAppService.UpdateAsync(perfilUsuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(perfilUsuario);
        }

        // POST: api/PerfilUsuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/perfilusuario")]
        [Authorize(Policy = "PostPerfilUsuario")]
        public async Task<IActionResult> PostPerfilUsuario(PerfilUsuarioViewModel perfilUsuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _perfilUsuarioAppService.RegisterAsync(perfilUsuario);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(perfilUsuario);
        }

        // DELETE: api/PerfilUsuario/5
        [HttpDelete("api/perfilusuario/{id}")]
        [Authorize(Policy = "DeletePerfilUsuario")]
        public async Task<IActionResult> DeletePerfilUsuario(Guid id)
        {
            if (!await _perfilUsuarioAppService.AnyAsync(id))
            {
                return NotFound();
            }

            await _perfilUsuarioAppService.RemoveAsync(id);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}

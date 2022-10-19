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
    public class ClienteController : BaseController
    {
        private readonly IClienteAppService _clienteAppService;
        private readonly IApiClienteAppService _apiClienteAppService;

        public ClienteController(
            IClienteAppService clienteAppService,
            IApiClienteAppService apiClienteAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _clienteAppService = clienteAppService;
            _apiClienteAppService = apiClienteAppService;
        }

        [HttpGet("api/cliente/")]
        [Authorize(Policy = "GetCliente")]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> GetCliente()
        {
            return Ok(await _clienteAppService.GetAllAsync());
        }

        [HttpGet("api/cliente/{id}")]
        [Authorize(Policy = "GetCliente")]
        public async Task<ActionResult<ClienteViewModel>> GetCliente(Guid id)
        {
            var cliente = await _clienteAppService.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }
               
        [HttpPut("api/cliente/{id}")]
        [Authorize(Policy = "PutCliente")]
        public async Task<IActionResult> PutCliente(Guid id, [FromBody] ClienteViewModel cliente)
        {
            if (id != cliente.Id)
                return BadRequest();

            if (!await _clienteAppService.AnyAsync(cliente.Id.GetValueOrDefault()))
            {
                return NotFound(cliente.Id);
            }

            await _clienteAppService.UpdateAsync(cliente);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(cliente);
        }

        [HttpPost("api/cliente/")]
        [Authorize(Policy = "PostCliente")]
        public async Task<IActionResult> PostCliente([FromBody] ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clienteAppService.RegisterAsync(cliente);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok(cliente);
        }   

        [HttpDelete("api/cliente/{id}")]
        [Authorize(Policy = "DeleteCliente")]
        public async Task<IActionResult> DeleteCliente(Guid id)
        {
            if (!await _clienteAppService.AnyAsync(id))
            {
                return NotFound();
            }

            await _clienteAppService.RemoveAsync(id);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        #region API
        [HttpGet("api/cliente/{id}/api")]
        [Authorize(Policy = "PutCliente")]
        public async Task<ActionResult<IEnumerable<ApiClienteViewModel>>> GetApisCliente(Guid id)
        {
            var cliente = await _apiClienteAppService.GetByIdClienteAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost("api/cliente/{id}/api")]
        [Authorize(Policy = "PutCliente")]
        public async Task<IActionResult> PostCliente(Guid id, [FromBody] ApiClienteViewModel apiCliente)
        {
            if (id != apiCliente.IdCliente)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _apiClienteAppService.RegisterAsync(apiCliente);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpDelete("api/cliente/api/{id}")]
        [Authorize(Policy = "PutCliente")]
        public async Task<IActionResult> DeleteApiCliente(Guid id)
        {
            if (!await _apiClienteAppService.AnyAsync(id))
            {
                return NotFound();
            }

            await _apiClienteAppService.RemoveAsync(id);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
        #endregion


    }
}

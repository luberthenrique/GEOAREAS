using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Presentation.Controllers
{
    [ApiController]
    public class SetoresCensitariosController : BaseController
    {
        private readonly ISetoresCensitariosAppService _SetoresCensitariosAppService;

        public SetoresCensitariosController(
            ISetoresCensitariosAppService SetoresCensitariosAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _SetoresCensitariosAppService = SetoresCensitariosAppService;
        }

        [HttpGet("api/setores-censitarios/")]
        [Authorize(Policy = "GetSetoresCensitarios")]        
        public async Task<ActionResult<IEnumerable<ArquivoSetoresCensitariosViewModel>>> GetSetoresCensitarios()
        {
            return Ok(await _SetoresCensitariosAppService.GetAllAsync());
        }

        [HttpPost("api/setores-censitarios")]
        [Authorize(Policy = "PostSetoresCensitarios")]
        [RequestSizeLimit(40000000000)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> PostSetoresCensitarios([FromForm] ArquivoSetoresCensitariosUploadViewModel obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _SetoresCensitariosAppService.RegisterAsync(obj);

            if (HasNotifications)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}

using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Geolocalizacao.Api.Controllers
{
    [ApiController]
    public class SetorController : BaseController
    {
        private readonly ISetorAppService _geolocalizacaoAppService;

        public SetorController(
            ISetorAppService geolocalizacaoAppService,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _geolocalizacaoAppService = geolocalizacaoAppService;
        }

        [HttpGet("api/area")]
        [Authorize]
        public ActionResult<IEnumerable<SetorViewModel>> GetSetor(double latitude, double longitude, int raio)
        {
            var setores = _geolocalizacaoAppService.GetByDistance(latitude, longitude, raio);
            
            return Ok(JsonConvert.SerializeObject(setores));
        }
    }
}

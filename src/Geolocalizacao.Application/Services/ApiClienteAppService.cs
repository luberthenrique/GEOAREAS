using AutoMapper;
using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Commands.Clientes;
using Geolocalizacao.Domain.Core.Bus;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Services
{
    public class ApiClienteAppService : ApplicationBaseService, IApiClienteAppService
    {
        private readonly IApiClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ApiClienteAppService(
            IApiClienteRepository repository,
            IMapper mapper,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await _repository.GetAll().AnyAsync(c => c.Id == id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<IEnumerable<ApiClienteViewModel>> GetByIdClienteAsync(Guid id)
        {
            return await _repository.GetAll()
                .Where(c => c.IdCliente == id)
                .Select(c => new ApiClienteViewModel
            {
                Id = c.Id,
                IdCliente = c.IdCliente,
                Nome = c.Nome,
                ApiKey = c.ApiKey,
                SecretKey = c.SecretKey,
                DataHoraInclusao = c.DataHoraInclusao,
                UsuarioInclusao = new UsuarioViewModel
                {
                    Nome = c.Usuario_Inclusao.Nome,
                    Email = c.Usuario_Inclusao.Email
                }
            }).ToListAsync();
        }

        public async Task RegisterAsync(ApiClienteViewModel obj)
        {
            var registerCommand = _mapper.Map<RegistrarApiClienteCommand>(new ApiClienteViewModel { IdCliente = obj.IdCliente, Nome = obj.Nome });
            await _mediator.SendCommand(registerCommand);
        }

        public async Task RemoveAsync(Guid id)
        {
            var registerCommand = _mapper.Map<RemoverApiClienteCommand>(new ApiClienteViewModel { Id = id });
            await _mediator.SendCommand(registerCommand);
        }

    }
}

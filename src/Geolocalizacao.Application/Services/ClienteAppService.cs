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
    public class ClienteAppService : ApplicationBaseService, IClienteAppService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ClienteAppService(
            IClienteRepository repository,
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

        public async Task<bool> AnyAsync(string cnpj)
        {
            return await _repository.GetAll().AnyAsync(c => c.Cnpj == cnpj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<IEnumerable<ClienteViewModel>> GetAllAsync()
        {
            return await _repository.GetAll().Select(c => new ClienteViewModel
            {
                Id = c.Id,
                Cnpj = c.Cnpj,
                RazaoSocial = c.RazaoSocial,
                Cidade = c.Cidade,
                Uf = c.Uf
            }).ToListAsync();
        }

        public async Task<ClienteViewModel> GetByIdAsync(Guid id)
        {
            return await _repository.GetAll().Select(c => new ClienteViewModel
            {
                Id = c.Id,
                Cnpj = c.Cnpj,
                InscricaoMunicipal = c.InscricaoMunicipal,
                RazaoSocial = c.RazaoSocial,
                Observacao = c.Observacao,
                Logradouro = c.Logradouro,
                Numero = c.Numero,
                Complemento = c.Complemento,
                Bairro = c.Bairro,
                Cidade = c.Cidade,
                Uf = c.Uf,
                Cep = c.Cep,
                Telefone1 = c.Telefone1,
                Telefone2 = c.Telefone2,
                Email = c.Email
            }).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ClienteViewModel> GetByCnpjAsync(string cnpj)
        {
            return await _repository.GetAll().Select(c => new ClienteViewModel
            {
                Id = c.Id,
                Cnpj = c.Cnpj,
                InscricaoMunicipal = c.InscricaoMunicipal,
                RazaoSocial = c.RazaoSocial,
                Observacao = c.Observacao,
                Logradouro = c.Logradouro,
                Numero = c.Numero,
                Complemento = c.Complemento,
                Bairro = c.Bairro,
                Cidade = c.Cidade,
                Uf = c.Uf,
                Cep = c.Cep,
                Telefone1 = c.Telefone1,
                Telefone2 = c.Telefone2,
                Email = c.Email
            }).FirstOrDefaultAsync(c => c.Cnpj == cnpj);
        }

        public async Task<ClienteViewModel> RegisterAsync(ClienteViewModel obj)
        {
            var registerCommand = _mapper.Map<RegistrarClienteCommand>(obj);
            await _mediator.SendCommand(registerCommand);

            return obj;
        }

        public async Task RemoveAsync(Guid id)
        {
            var removeCommand = _mapper.Map<RemoverClienteCommand>(new ClienteViewModel { Id = id});
            await _mediator.SendCommand(removeCommand);
        }

        public async Task<ClienteViewModel> UpdateAsync(ClienteViewModel obj)
        {
            var alterarCommand = _mapper.Map<AlterarClienteCommand>(obj);
            await _mediator.SendCommand(alterarCommand);

            return obj;
        }

    }
}

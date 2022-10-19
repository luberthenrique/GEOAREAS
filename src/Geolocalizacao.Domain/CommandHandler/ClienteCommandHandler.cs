using Geolocalizacao.Domain.Commands.Clientes;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.CommandHandler
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<RegistrarClienteCommand, bool>,
        IRequestHandler<AlterarClienteCommand, bool>,
        IRequestHandler<RemoverClienteCommand, bool>
    {
        private readonly IClienteRepository _repository;
        private readonly IApiClienteRepository _apiClienteRepository;
        private readonly IUser _user;

        public ClienteCommandHandler(
            IClienteRepository repository,
            IApiClienteRepository apiClienteRepository,
            IUser user,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _repository = repository;
            _apiClienteRepository = apiClienteRepository;
            _user = user;
        }

        public async Task<bool> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            if (_repository.GetAll().Where(c => c.Cnpj == message.Cnpj).Any())
                AddNotification("", "O CNPJ está vincualado a um cliente cadastrado no sistema.");

            if (HasNotifications())
                return await Task.FromResult(false);

            var cliente = new Cliente(
                Guid.NewGuid(),
                message.Cnpj,
                message.InscricaoMunicipal,
                message.RazaoSocial,
                message.Observacao,
                message.Logradouro,
                message.Numero,
                message.Complemento,
                message.Bairro,
                message.Cidade,
                message.Uf,
                message.Cep,
                message.Telefone1,
                message.Telefone2,
                message.Email,
                _user.Id,
                DateTime.Now,
                null,
                null
                );

            await _repository.Add(cliente);
            await Commit();

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(AlterarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }
            var clienteBd = await _repository.GetByIdAsync(message.Id);
            if (clienteBd is null)
                AddNotification("", "O Cliente informado não pode ser localizado no sistema.");

            if (_repository.GetAll().Where(c => c.Cnpj == message.Cnpj && c.Id != message.Id).Any())
                AddNotification("", "O CNPJ está vincualado a um cliente cadastrado no sistema.");

            if (HasNotifications())
                return await Task.FromResult(false);

            var cliente = new Cliente(
                message.Id,
                message.Cnpj,
                message.InscricaoMunicipal,
                message.RazaoSocial,
                message.Observacao,
                message.Logradouro,
                message.Numero,
                message.Complemento,
                message.Bairro,
                message.Cidade,
                message.Uf,
                message.Cep,
                message.Telefone1,
                message.Telefone2,
                message.Email,
                clienteBd.IdUsuarioInclusao,
                clienteBd.DataHoraInclusao,
                _user.Id,
                DateTime.Now
                );

            _repository.Update(cliente);
            await Commit();

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(RemoverClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            if (HasNotifications())
                return await Task.FromResult(false);

            var apis = _apiClienteRepository.GetAll().Where(c => c.IdCliente == message.Id).Select(c=> c.Id).ToList();
            foreach (var item in apis)
            {
                await _apiClienteRepository.RemoveAsync(item);
            }

            await _repository.RemoveAsync(message.Id);
            await Commit();

            return await Task.FromResult(true);
        }
    }
}

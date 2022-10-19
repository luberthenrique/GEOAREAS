using Geolocalizacao.Domain.Commands.Clientes;
using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using MediatR;
using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.CommandHandler
{
    public class ApiClienteCommandHandler : CommandHandler,
        IRequestHandler<RegistrarApiClienteCommand, bool>,
        IRequestHandler<RemoverApiClienteCommand, bool>
    {
        private readonly IApiClienteRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUser _user;

        public ApiClienteCommandHandler(
            IApiClienteRepository repository,
            IClienteRepository clienteRepository,
            IUser user,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications) : base(uow, notifications)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _user = user;
        }

        public async Task<bool> Handle(RegistrarApiClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            if (_repository.GetAll().Any(c => c.Nome == message.Nome))
            {
                AddNotification("", "Uma API já foi cadastrada como o nome informado.");
            }

            var clienteBd = await _clienteRepository.GetByIdAsync(message.IdCliente);
            if (clienteBd is null)
                AddNotification("", "O Cliente informado não pode ser localizado no sistema.");

            if (HasNotifications())
                return await Task.FromResult(false);

            var api = new ApiCliente(
                Guid.NewGuid(),
                message.IdCliente,
                message.Nome,
                CreateKey(),
                CreateKey(),
                _user.Id,
                DateTime.Now,
                null,
                null
                );

            await _repository.Add(api);
            await Commit();

            return await Task.FromResult(true);
        }

        public async Task<bool> Handle(RemoverApiClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return await Task.FromResult(false);
            }

            if (!_repository.GetAll().Any(c => c.Id == message.Id))
            {
                AddNotification("", "A API informada não pode ser localizado no sistema.");
            }

            if (HasNotifications())
                return await Task.FromResult(false);

            await _repository.RemoveAsync(message.Id);
            await Commit();

            return await Task.FromResult(true);
        }

        private string CreateKey()
        {
            var bytes = new byte[256 / 8];
            using (var random = RandomNumberGenerator.Create())
                random.GetBytes(bytes);
            return ToBase62String(bytes);
        }

        private string ToBase62String(byte[] toConvert)
        {
            const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            BigInteger dividend = new BigInteger(toConvert);
            var builder = new StringBuilder();
            while (dividend != 0)
            {
                dividend = BigInteger.DivRem(dividend, alphabet.Length, out BigInteger remainder);
                builder.Insert(0, alphabet[Math.Abs(((int)remainder))]);
            }
            return builder.ToString();
        }
    }
}

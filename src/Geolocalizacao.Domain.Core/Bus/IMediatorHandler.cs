using System.Threading.Tasks;
using Geolocalizacao.Domain.Core.Commands;
using Geolocalizacao.Domain.Core.Events;


namespace Geolocalizacao.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}

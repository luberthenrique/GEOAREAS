using System;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}

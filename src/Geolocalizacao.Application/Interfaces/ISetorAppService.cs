using Geolocalizacao.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Application.Interfaces
{
    public interface ISetorAppService : IDisposable
    {
        List<SetorViewModel> GetByDistance(double latitude, double longitude, int distancia);
    }
}

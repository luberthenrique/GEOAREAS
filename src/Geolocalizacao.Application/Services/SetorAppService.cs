using Geolocalizacao.Application.Interfaces;
using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Domain.Core.Notifications;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Geolocalizacao.Application.Services
{
    public class SetorAppService : ApplicationBaseService, ISetorAppService
    {
        private readonly ISetorRepository _repository;

        public SetorAppService(
            ISetorRepository repository,
            INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public List<SetorViewModel> GetByDistance(double latitude, double longitude, int distancia)
        {
            var setores = _repository.GetByDistance(latitude, longitude, distancia);

            // Obter somente os polígonos externos de cada setor
            return setores
                .Select(c => new SetorViewModel
                {
                    Id = c.Id,
                    FeatureId = c.FeatureId,
                    Geometry = new GeometryViewModel
                    {
                        Type = c.Geometry.Type,
                        Coordinates =
                        c.Geometry is Polygon
                            ? (c.Geometry as Polygon).Coordinates
                            : (c.Geometry as MultiPolygon).Coordinates
                    },
                    Codigo = c.Codigo,
                    CodigoSituacao = c.CodigoSituacao,
                    Situacao = c.Situacao,
                    CodigoUf = c.CodigoUf,
                    NomeUf = c.NomeUf,
                    Uf = c.Uf,
                    CodigoMunicipio = c.CodigoMunicipio,
                    Municipio = c.Municipio,
                    CodigoDistrito = c.CodigoDistrito,
                    NomeDistrito = c.NomeDistrito,
                    CodigoSubDistrito = c.CodigoSubDistrito,
                    NomeSubDistrito = c.NomeSubDistrito
                }).ToList();
        }
    }
}

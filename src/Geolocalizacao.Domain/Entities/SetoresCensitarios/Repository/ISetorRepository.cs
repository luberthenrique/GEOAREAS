using Geolocalizacao.Domain.Core.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository
{
    public interface ISetorRepository: INoSqlRepository<Setor>
    {
        List<Setor> Find(Expression<Func<Setor, bool>> expression);
        List<Setor> GetByDistance(double x, double y, double? maxDistance = null);
        DeleteResult DeletarSetores(string uf);
    }
}

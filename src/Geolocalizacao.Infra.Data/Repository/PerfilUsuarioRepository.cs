using Microsoft.EntityFrameworkCore;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using Geolocalizacao.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.Data.Repository
{
    public class PerfilUsuarioRepository : IPerfilUsuarioRepository
    {
        protected ApplicationDbContext Db;
        protected DbSet<PerfilUsuario> DbSet;

        public PerfilUsuarioRepository(ApplicationDbContext dbContext)
        {
            Db = dbContext;
            DbSet = Db.Set<PerfilUsuario>();
        }
        public virtual void Update(PerfilUsuario obj) => DbSet.Update(obj);
        public async virtual Task<bool> AnyAsync(Expression<Func<PerfilUsuario, bool>> predicate) => await DbSet.AsNoTracking().AnyAsync(predicate);

        public async virtual Task<PerfilUsuario> GetByIdAsync(Guid id) => await DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        public async virtual Task DeleteAsync(Guid id)
        {
            var obj = await DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            obj.Deletar();
            DbSet.Update(obj);
        }

        public virtual IQueryable<PerfilUsuario> GetAll() => DbSet.AsNoTracking();
        public async Task<int> SaveChangesAsync() => await Db.SaveChangesAsync();

        public void Dispose() => Db.Dispose();

    }
}

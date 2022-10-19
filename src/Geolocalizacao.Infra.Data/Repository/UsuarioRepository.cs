using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Entities.Usuarios.Repository;
using Geolocalizacao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected ApplicationDbContext Db;
        protected DbSet<Usuario> DbSet;
        private readonly IUser _user;
        public UsuarioRepository(ApplicationDbContext dbContext, IUser user)
        {
            Db = dbContext;
            DbSet = Db.Set<Usuario>();
            _user = user;
        }

        public virtual Task<bool> AnyAsync(Expression<Func<Usuario, bool>> predicate) => DbSet.AsNoTracking().AnyAsync(predicate);
        public virtual Task<bool> AnyAsyncNoQuery(Expression<Func<Usuario, bool>> predicate) => DbSet.AsNoTracking().IgnoreQueryFilters().AnyAsync(predicate);
        public virtual async Task<bool> IsAdmin(Guid id) => await DbSet.AsNoTracking().Where(c=> c.Id == id).Select(c=> c.PerfilUsuario.IsAdmin).FirstOrDefaultAsync();

        public virtual IQueryable<Usuario> GetAll() 
        {
            return DbSet.AsNoTracking();  
        }
        public virtual Task<Usuario> GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }
        public async virtual Task RemoveAsync(Guid id)
        {
            var obj = await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            obj.Deletar();
            DbSet.Update(obj);
        }
        public virtual void Update(Usuario usuario)
        {
            DbSet.Update(usuario);
        }
        public async Task<int> SaveChangesAsync() => await Db.SaveChangesAsync();
        public void Dispose() => Db.Dispose();

        private IEnumerable<Usuario> GetChildren(Usuario usuario)
        {
            foreach (var rChild in usuario.UsuarioVinculo.SelectMany(child => GetChildren(child)))
            {
                yield return rChild;
            }
        }
        public IEnumerable<Guid> Recursive(Guid id)
        {
            var list = DbSet.AsNoTracking().Where(c => c.IdUsuario == id).SelectMany(c => Recursive(c.Id).AsEnumerable()).ToList();

            return DbSet.Where(c => c.Id == id).Select(c => c.Id).ToList().Union(list);
        }

        public IQueryable<Usuario> Recursive(Usuario usuario)
        {
            var list = DbSet.AsNoTracking().Where(c => c.IdUsuario == usuario.Id).SelectMany(c=> Recursive(c).ToList());

            return list.Append(usuario);
        }

        public async Task<IEnumerable<Guid>> ObterSubordinados(Guid id)
        {
            var subordinados = new List<Guid>();

            foreach (var item in await DbSet.AsNoTracking().Where(c => c.IdUsuario == id).Select(c => c.Id).ToListAsync())
            {
                subordinados.AddRange(await ObterSubordinados(item));
            }

            subordinados.Add(id);

            return subordinados.OrderBy(c=> c);
        }
    }
}

using Geolocalizacao.Domain.Core.Interfaces;
using Geolocalizacao.Domain.Core.Models;
using Geolocalizacao.Infra.Data.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.Data.Repository.Base
{
    public class NoSqlRepository<TEntity> : INoSqlRepository<TEntity> where TEntity : Entity
    {
        protected readonly NoSqlDbContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        public NoSqlRepository(NoSqlDbContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Add(TEntity obj)
        {
            await DbSet.InsertOneAsync(obj);
        }

        public async Task AddRange(IEnumerable<TEntity> obj)
        {
            await DbSet.InsertManyAsync(obj);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Where(expression));
            return data.Any();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable<TEntity>();
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj);
            //_context.AddCommand(async () =>
            //{
            //    await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id), obj);
            //});
        }

        public async Task RemoveAsync(Guid id) => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChanges();
        }
    }
}

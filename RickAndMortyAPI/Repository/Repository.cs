using RickAndMortyAPI.Intefaces;
using RickAndMortyAPI.Models;
using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.Data;
using System.Linq.Expressions;

namespace RickAndMortyAPI.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApplicationDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await Save();
        }

        public virtual async Task Create(TEntity entity)
        {
            DbSet.Add(entity);
            await Save();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public virtual async Task Set(TEntity entity)
        {
            DbSet.Update(entity);
            await Save();
        }   

        public async Task<int> Save()
        {
            return await Db.SaveChangesAsync();
        }
       
    }
}
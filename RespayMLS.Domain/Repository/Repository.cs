using Microsoft.EntityFrameworkCore;
using RespayMLS.Core.Interface;
using RespayMLS.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RespayMLS.Domain.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RespayDbContext _respayDbContext;

        public Repository(RespayDbContext respayDbContext)
        {
            _respayDbContext = respayDbContext;
        }
        public async Task<IEnumerable<T>> GetAll() => await _respayDbContext.Set<T>().ToListAsync();

        public async Task<T> Add(T entity)
        {
            await _respayDbContext.Set<T>().AddAsync(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            _respayDbContext.Set<T>().Remove(entity);
        }

        public async Task<T> GetById(int Id)
        {
            return await _respayDbContext.Set<T>().FindAsync(Id);
        }

        public T Update(T entity)
        {
            _respayDbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll(string Navigation)
        {
            return await _respayDbContext.Set<T>().Include(Navigation).ToListAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RespayMLS.Core.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int Id);


        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(string Navigation);

        Task<T> Add(T entity);

        T Update(T entity);

        void Delete(T entity);
    }
}

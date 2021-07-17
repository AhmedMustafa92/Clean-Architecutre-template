using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhatsBack.Application.Interfaces.Repositories
{
    public interface ICreateRepository<T> where T : class
    {
        #region Insert
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        void BulkInsert(List<T> entities);
        #endregion
    }
}

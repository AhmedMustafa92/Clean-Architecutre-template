using System;
using System.Linq.Expressions;

namespace WhatsBack.Application.Interfaces.Repositories
{
    public interface IDeleteRepository<T> where T : class
    {
        #region Delete
        void BulkHardDelete(Expression<Func<T, bool>> filter = null);
        void Delete(T entity);
        #endregion
    }
}

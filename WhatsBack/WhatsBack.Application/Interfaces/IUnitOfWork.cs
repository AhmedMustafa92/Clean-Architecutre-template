using System;
using System.Threading.Tasks;

namespace WhatsBack.Application.Interfaces
{
    public partial interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit changes
        /// </summary>
        Task<bool> Commit();
        Task<int> Commit(string _culture);
        Task<bool> Transaction(Action action);
    }
}

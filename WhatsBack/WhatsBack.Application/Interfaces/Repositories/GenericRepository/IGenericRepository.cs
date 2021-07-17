namespace WhatsBack.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> : ICreateRepository<T>, IUpdateRepository<T>, IDeleteRepository<T>, IRetrieveRepository<T> where T : class
    {
    }
}

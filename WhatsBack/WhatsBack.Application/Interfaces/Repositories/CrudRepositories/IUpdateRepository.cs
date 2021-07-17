namespace WhatsBack.Application.Interfaces.Repositories
{
    public interface IUpdateRepository<T> where T : class
    {
        void Update(T entity);
    }
}

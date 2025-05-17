namespace TaskManager.Core.Query.Base
{
    public interface IQueryRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();

    }
}

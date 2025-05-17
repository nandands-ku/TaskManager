using TaskManager.Core.Entities;
using TaskManager.Core.Query.Base;

namespace TaskManager.Core.Query
{
    public interface IUserQueryRepository : IQueryRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

    }
}

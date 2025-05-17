using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repository.Query.Base
{
    public class QueryBaseRepository<T> : Core.Query.Base.IQueryRepository<T> where T : class
    {
        private readonly TaskManagerDBContext _dBcontext;
        private readonly DbSet<T> _dBSet;
        public QueryBaseRepository(TaskManagerDBContext dBContext)
        {
            _dBcontext = dBContext;
            _dBSet = _dBcontext.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dBSet.ToListAsync();
        }

        public Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

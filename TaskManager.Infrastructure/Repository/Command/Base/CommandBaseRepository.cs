using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Command.Base;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repository.Command.Base
{
    public class CommandBaseRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly TaskManagerDBContext _dBContext;
        public CommandBaseRepository(TaskManagerDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dBContext.AddAsync(entity);
            await _dBContext.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dBContext.Set<T>().Remove(entity);
            return Task.CompletedTask;

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return entity;
        }
    }
}

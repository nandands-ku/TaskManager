using TaskManager.Core.Command;
using TaskManager.Core.Command.Base;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Command.Base;

namespace TaskManager.Infrastructure.Repository.Command
{
    public class UserCommandRepository : CommandBaseRepository<User>, IUserCommandRepository
    {
        public UserCommandRepository(TaskManagerDBContext dBContext) : base(dBContext)
        {

        }
    }
}

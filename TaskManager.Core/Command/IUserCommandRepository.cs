using TaskManager.Core.Command.Base;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Command
{
    public interface IUserCommandRepository : ICommandRepository<User>
    {
    }
}

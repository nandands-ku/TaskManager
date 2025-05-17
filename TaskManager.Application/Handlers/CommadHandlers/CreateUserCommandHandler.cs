using MediatR;
using TaskManager.Application.Commands.User;
using TaskManager.Application.Mapper;
using TaskManager.Application.Response;
using TaskManager.Core.Command;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Handlers.CommadHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserCommandRepository _repository;

        public CreateUserCommandHandler(IUserCommandRepository userRepository)
        {
            _repository = userRepository;
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = TaskManagerProfile.Mapper.Map<User>(request);
            var response = await _repository.CreateAsync(entity);
            var commandResponse = TaskManagerProfile.Mapper.Map<CreateUserResponse>(response);

            return commandResponse;

        }
    }
}

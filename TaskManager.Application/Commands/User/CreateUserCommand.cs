using MediatR;
using TaskManager.Application.Response;

namespace TaskManager.Application.Commands.User
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

    }
}

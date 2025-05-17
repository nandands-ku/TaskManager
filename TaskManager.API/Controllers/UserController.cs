using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.User;
using TaskManager.Application.Response;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        //[Authorize(Roles = "Admin")]
        public async Task<CreateUserResponse> CreateUser([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("Created User!");

            var response = await _mediator.Send(command);
            return response;
        }
    }
}

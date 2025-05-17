using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands.User;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly IValidator<CreateUserCommand> _validator;
        public UserController(IMediator mediator, ILogger<UserController> logger
            , IValidator<CreateUserCommand> validator)
        {
            _mediator = mediator;
            _logger = logger;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                _logger.LogInformation("Created User!");

                var response = await _mediator.Send(command);

                return Ok(response);
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }
    }
}

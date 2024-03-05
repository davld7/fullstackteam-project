using Ioon.Application.UsesCases.Users.Commands;
using Ioon.Domain.Common.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace API.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]/")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IEmailService _emailService;
        private readonly ISender _mediator;

        public UserController(ILogger<UserController> logger, IEmailService emailService, ISender mediator)
        {
            _logger = logger;
            _emailService = emailService;
            _mediator = mediator;
        }

        [HttpPost("AuthenticateEmail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email)
        {
            var result = await _emailService.SendEmailCodeVerificationAsync(email);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
     
        }
    }
    
}

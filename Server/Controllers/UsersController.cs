using Application.Commons.Models;
using Application.Features.Users.Commands.AuthenticateUser;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.GetUserById;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<Response<AuthenticationResponse>>> Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<AuthenticationResponse>(ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToArray()));
            }

            var command = new AuthenticateUserCommand()
            {
                Password = request.Password,
                Email = request.Email,
                IpAddress = GenerateIpAddress()
            };
            var response = await _sender.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Response<UserResponse>>> GetById(string id)
        {
            var response = await _sender.Send(new GetUserByIdQuery(id));
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<Response<string>>> Create([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<string>(ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToArray()));
            }

            var command = new CreateUserCommand()
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                Email = request.Email,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers["origin"]!
            };
            var response = await _sender.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-for"))
                return Request.Headers["X-Forwarded-for"]!;
            else
                return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        }
    }
}

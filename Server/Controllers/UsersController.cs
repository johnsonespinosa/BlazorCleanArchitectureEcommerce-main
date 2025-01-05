using Application.Commons.Models;
using Application.Features.Users.Commands.AuthenticateUser;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        [HttpPost(template: "Authenticate")]
        public async Task<ActionResult<Response<AuthenticationResponse>>> Authenticate([FromBody] AuthenticationRequest request)
        {
            var command = new AuthenticateUserCommand()
            {
                Password = request.Password,
                Email = request.Email,
                IpAddress = GenerateIpAddress()
            };
            var response = await sender.Send(command);

            if (!response.Succeeded)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Data!.Id }, value: response);
        }

        [HttpGet(template: "GetById/{id}")]
        public async Task<ActionResult<Response<UserResponse>>> GetById(string id)
        {
            var request = new GetUserByIdQuery(id);
            var response = await sender.Send(request);
            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpPost(template: "Create")]
        public async Task<ActionResult<Response<string>>> Create([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand()
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                Email = request.Email,
                ConfirmPassword = request.ConfirmPassword,
                Origin = Request.Headers["origin"]!
            };
            var response = await sender.Send(command);

            if (!response.Succeeded)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Data },value: response);
        }

        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-for"))
                return Request.Headers["X-Forwarded-for"]!;
            
            return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        }
    }
}

using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Response<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateUserAsync(new UserRequest
            {
                ConfirmPassword = request.ConfirmPassword,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                UserName = request.UserName
            }, request.Origin!);
        }
    }
}

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(expression: command => command.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("The email format is not valid.");

            RuleFor(expression: command => command.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("The password must be at least 6 characters.");
        }
    }
}

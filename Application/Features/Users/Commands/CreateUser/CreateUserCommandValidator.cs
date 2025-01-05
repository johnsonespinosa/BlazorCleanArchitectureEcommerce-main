using Application.Commons.Models;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(expression: request => request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("The email format is not valid.");

            RuleFor(expression: request => request.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("The username must be between 3 and 50 characters.");

            RuleFor(expression: request => request.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(expression: @"^\+?[1-9]\d{1,14}$").WithMessage("The phone number is invalid.");

            RuleFor(expression: request => request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("The password must be at least 6 characters.");

            RuleFor(expression: request => request.ConfirmPassword)
                .Equal(expression: request => request.Password).WithMessage("The passwords do not match.");
        }
    }
}

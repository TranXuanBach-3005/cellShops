using cellShopSloution.ViewModel.Dtos.Users;
using FluentValidation;

namespace cellShopSloution.ViewModel.FluentValidation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is requierd")
                                    .MinimumLength(8).WithMessage("Password at least 8 characters");

        }
    }
}

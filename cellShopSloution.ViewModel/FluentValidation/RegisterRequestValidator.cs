using cellShopSloution.ViewModel.Dtos.Users;
using FluentValidation;

namespace cellShopSloution.ViewModel.FluentValidation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email fomat not match");
            RuleFor(x => x.BirthDay).GreaterThan(DateTime.Now.AddYears(-110));
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phon is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is requierd")
                                    .MinimumLength(8).WithMessage("Password at least 8 characters");
            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });
        }
    }
}

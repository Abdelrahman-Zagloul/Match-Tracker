namespace MatchTracker.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("User Name is required.")
                .WithName("User Name");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.")
                .WithName("Confirm Password");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage("Invalid Phone Number");
        }
    }
}

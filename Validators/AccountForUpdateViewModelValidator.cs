namespace MatchTracker.Validators
{
    public class AccountForUpdateViewModelValidator: AbstractValidator<AccountForUpdateViewModel>
    {
        public AccountForUpdateViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                 .Matches(@"^01[0125][0-9]{8}$")
                 .WithMessage("Invalid Phone Number");


            RuleFor(x => x.Role)
                .NotNull().WithMessage("Role is required.");
        }
    }
}

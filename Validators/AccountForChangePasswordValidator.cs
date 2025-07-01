namespace MatchTracker.Validators
{
    public class AccountForChangePasswordValidator:AbstractValidator<AccountForChangePassword>
    {
        public AccountForChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old Password is required.")
                .MinimumLength(6).WithMessage("Old Password must be at least 6 characters long.");
            RuleFor(x => x.NewPassword)

                .NotEmpty().WithMessage("New Password is required.")
                .MinimumLength(6).WithMessage("New Password must be at least 6 characters long.")
                .NotEqual(x => x.OldPassword).WithMessage("New Password cannot be the same as Old Password.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.NewPassword).WithMessage("Confirm Password must match New Password.");
        }
    }
}

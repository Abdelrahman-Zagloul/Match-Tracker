namespace MatchTracker.Validators
{
    public class PlayerForAddViewModelValidator:AbstractValidator<PlayerForAddViewModel>
    {
        public PlayerForAddViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Age)
                .NotEmpty() 
                .WithMessage("Age is required.")
                .InclusiveBetween(18, 40)
                .WithMessage("Age must be between 18 and 40.");

            RuleFor(x => x.Assists)
               .NotEmpty()
               .WithMessage("Assists is required.")
               .InclusiveBetween(1,2000)
               .WithMessage("Assists must be between 1 and 2000.");

            RuleFor(x => x.GoalsScored)
               .NotEmpty()
               .WithMessage("Goals Scored is required.")
               .InclusiveBetween(1, 2000)
               .WithMessage("Goals Scored must be between 1 and 2000.");

            RuleFor(x => x.MatchesPlayed)
               .NotEmpty()
               .WithMessage("Goals Scored is required.")
               .InclusiveBetween(1, 2000)
               .WithMessage("Goals Scored must be between 1 and 2000.");

            RuleFor(x => x.TeamId)
                .NotEmpty()
                .WithMessage("Team ID is required.");
        }
    }
}

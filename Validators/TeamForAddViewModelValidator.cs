
namespace MatchTracker.Validators
{
    public class TeamForAddViewModelValidator : AbstractValidator<TeamForAddViewModel>
    {
        public TeamForAddViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Team Name is required.");



            RuleFor(x => x.CoachName)
                .NotEmpty().WithMessage("Coach Name is required.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.FoundedYear)
                .InclusiveBetween(1800, DateTime.Now.Year).WithMessage("Founded year must be between 1800 and the current year.");

            RuleFor(x => x.ChampionshipsWon)
                .GreaterThanOrEqualTo(0).WithMessage("Championships won cannot be negative.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("Logo file is required.")
                .Must(file => file.Length > 0).WithMessage("Logo file cannot be empty.");
          
        }
    }
}

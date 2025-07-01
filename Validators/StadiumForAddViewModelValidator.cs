namespace MatchTracker.Validators
{
    public class StadiumForAddViewModelValidator: AbstractValidator<StadiumForAddViewModel>
    {
        public StadiumForAddViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Stadium name is required.")
                .MaximumLength(100).WithMessage("Stadium name cannot exceed 100 characters.");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");
           
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
            RuleFor(x => x.TeamId)
                .NotNull().WithMessage("Team selection is required.");
        }
       
    }
}

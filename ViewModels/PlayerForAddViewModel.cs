namespace MatchTracker.ViewModels
{
    public class PlayerForAddViewModel
    {
        public string Name { get; set; } = default!;
        public string Nationality { get; set; } = default!;
        public int Number { get; set; }
        public int Age { get; set; }
        public int MatchesPlayed { get; set; }
        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public Position Position { get; set; }

        public IFormFile ImageFile { get; set; } = default!;
        public int TeamId { get; set; }
        public List<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Teams { get; set; } = new List<SelectListItem>();
    }
}

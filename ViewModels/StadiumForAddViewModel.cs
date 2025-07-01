
namespace MatchTracker.ViewModels
{
    public class StadiumForAddViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int TeamId { get; set; }
        public List<SelectListItem> Teams { get; set; } = new List<SelectListItem>();
    }
}

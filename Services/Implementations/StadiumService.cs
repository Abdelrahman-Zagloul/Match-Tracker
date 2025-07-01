namespace MatchTracker.Services.Implementations
{
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumRepository _stadiumRepository;
        public StadiumService(IStadiumRepository stadiumRepository)
        {
            _stadiumRepository = stadiumRepository;
        }
        public void Add(StadiumForAddViewModel StadiumVM)
        {
            Stadium stadium = new Stadium
            {
                Name = StadiumVM.Name,
                Location = StadiumVM.Location,
                Capacity = StadiumVM.Capacity,
                TeamId = StadiumVM.TeamId,
            };
            try
            {
                _stadiumRepository.Add(stadium);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding stadium: {ex.Message}");
            }
        }
        public void Update(int id, StadiumForUpdateViewModel stadiumVM)
        {
            var stadium = new Stadium
            {
                Id = stadiumVM.Id ?? 0,
                Name = stadiumVM.Name,
                Location = stadiumVM.Location,
                Capacity = stadiumVM.Capacity ?? 0,
                TeamId = stadiumVM.TeamId
            };
            try
            {
                _stadiumRepository.Update(id, stadium);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating stadium: {ex.Message}");
            }
        }
        public bool Delete(int id)
        {
            try
            {
                return _stadiumRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting stadium: {ex.Message}");
            }
        }
        public bool Clear()
        {
            return _stadiumRepository.Clear();
        }
        public List<Stadium> GetAll()
        {
            return _stadiumRepository.GetAll();
        }

        public Stadium? GetById(int id)
        {
            try
            {
                return _stadiumRepository.GetById(id);
            }
            catch
            {
                throw new Exception($"Stadium with ID {id} not found.");
            }
        }
        public List<SelectListItem> GetTeamsWithoutStadiums()
        {
            var SelectListItem = _stadiumRepository
                .GetTeamsWithoutStadium()
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

            return SelectListItem;
        }
    }
}

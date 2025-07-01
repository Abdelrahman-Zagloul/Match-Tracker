namespace MatchTracker.Services.Interfaces
{
    public interface IStadiumService
    {
        void Add(StadiumForAddViewModel stadium);
        Stadium? GetById(int id);
        List<Stadium> GetAll();
        void Update(int id, StadiumForUpdateViewModel stadiumVM);
        bool Delete(int id);
        bool Clear();
        List<SelectListItem> GetTeamsWithoutStadiums();
    }
}

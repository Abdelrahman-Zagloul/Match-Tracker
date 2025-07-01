namespace MatchTracker.Services.Interfaces
{
    public interface ITeamService
    {
        void Add(TeamForAddViewModel team);
        Team? GetById(int id);
        List<Team> GetAll();
        void Update(int id, TeamForUpdateViewModel newVM);
        bool Delete(int id);
        bool Clear();
        Team? SearchByName(string searchTerm);
        List<Team> GetTeamsByCountry(string country);

    }
}

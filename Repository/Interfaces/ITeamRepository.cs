namespace MatchTracker.Repository.Interfaces
{
    public interface ITeamRepository
    {
        void Add(Team team);
        Team? GetById(int id);
        List<Team> GetAll();
        void Update(int id,Team newTeam);
        bool Delete(int id);
        bool Clear();
        Team? SearchByName(string searchTerm);
        List<Team> GetTeamsByCountry(string country);
    }
}

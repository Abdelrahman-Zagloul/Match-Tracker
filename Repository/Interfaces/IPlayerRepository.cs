
namespace MatchTracker.Repository.Interfaces
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Update(int id, Player player);
        bool Delete(int id);
        bool Clear();
        List<Player> GetAll();
        Player? GetById(int id);
        Player ?GetByName(string playerName);
        List<Player> GetByTeamName(string teamName);
        List<Player> GetByCountryName(string countryName);
        List<Team> GetAllTeams();
        string GetPictureName(int id);
    }
}

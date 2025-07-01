namespace MatchTracker.Repository.Interfaces
{
    public interface IMatchRepository
    {
        void Add(Match match);
        void Update(int id, Match newMatch);
        bool Delete(int id);
        bool Clear();
        Match? GetById(int id);
        List<Match> GetAll();
        List<Match> GetByDate(DateTime date);
        List<Match> GetByTeamName(string teamName);
        List<Team> GetAllTeams();
        public Stadium? GetStadiumByTeamId(int teamId);

    }
}

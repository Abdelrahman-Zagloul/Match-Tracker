namespace MatchTracker.Services.Interfaces
{
    public interface IMatchService
    {
        void Add(MatchForAddViewModel matchVM);
        void Update(int id, MatchForUpdateViewModel newMatch);
        bool Delete(int id);
        bool Clear();
        Match? GetById(int id);
        List<Match> GetAll();
        List<Match> GetByDate(DateTime date);
        List<Match> GetByTeamName(string teamName);
        List<SelectListItem> GetTeams();

        string GetStadiumNameByTeamId(int teamId);
        string GetStadiumLocationByTeamId(int teamId);
    }
}

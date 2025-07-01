namespace MatchTracker.Services.Interfaces
{
    public interface IPlayerService
    {
        void Add(PlayerForAddViewModel playerVM);
        Player? GetById(int id);
        List<Player> GetAll();
        void Update(int id, PlayerForUpdateViewModel playerVM);
        bool Delete(int id);
        bool Clear();

        Player ?GetByName(string playerName);
        List<Player> GetByTeamName(string teamName);
        List<Player> GetByCountryName(string countryName);


        string GetPictureName(int id);
        List<SelectListItem> GetTeams();
        List<SelectListItem> GetPositions();

    }
}


namespace MatchTracker.Repository.Implementations
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Player player)
        {
            try
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the Player.", ex);
            }
        }
        public void Update(int id, Player player)
        {
            var existingPlayer = _context.Players.Find(id);

            if (existingPlayer == null)
                throw new Exception("Player not found.");

            existingPlayer.Name = player.Name;
            existingPlayer.Number = player.Number;
            existingPlayer.Age = player.Age;
            existingPlayer.MatchesPlayed = player.MatchesPlayed;
            existingPlayer.GoalsScored = player.GoalsScored;
            existingPlayer.Assists = player.Assists;
            existingPlayer.Position = player.Position;
            existingPlayer.PicturePath = player.PicturePath;
            existingPlayer.TeamId = player.TeamId;

            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null)
                return false;

            _context.Players.Remove(player);
            return _context.SaveChanges() > 0;
        }
        public bool Clear()
        {
            return _context.Players.ExecuteDelete() > 0;
        }

        public List<Player> GetAll()
        {
            return _context.Players
                .Include(p => p.Team)
                .ToList();
        }

        public Player? GetById(int id)
        {
            return _context.Players
                .Include(p => p.Team)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Team> GetAllTeams()
        {
            return _context.Teams.AsNoTracking().ToList();
        }
        public Player? GetByName(string playerName)
        {
            return _context.Players
                .Include(p => p.Team).FirstOrDefault(x => x.Name == playerName);
        }
        public List<Player> GetByTeamName(string teamName)
        {
            return _context.Players
                .Include(p => p.Team)
                .Where(p => p.Team.Name == teamName)
                .ToList();
        }
        public List<Player> GetByCountryName(string countryName)
        {

            return _context.Players
                .Include(p => p.Team)
                .Where(p => p.Nationality == countryName)
                .OrderByDescending(p => p.GoalsScored)
                .ToList();
        }

        public string GetPictureName(int id)
        {
            var player = _context.Players.AsNoTracking().FirstOrDefault(p => p.Id == id);
            return player?.PicturePath ?? string.Empty;
        }
    }
}

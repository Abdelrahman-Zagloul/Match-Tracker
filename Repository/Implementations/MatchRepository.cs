

namespace MatchTracker.Repository.Implementations
{
    public class MatchRepository : IMatchRepository
    {
        private readonly AppDbContext _context;
        public MatchRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public bool Clear()
        {
            return _context.Matches.ExecuteDelete() > 0;
            //return _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Matches") > 0;
        }

        public bool Delete(int id)
        {
            var match = _context.Matches.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (match == null)
            {
                return false;
            }
            _context.Matches.Remove(match);
            return _context.SaveChanges() > 0;
        }

        public List<Match> GetAll()
        {
            return _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                //.Include(m => m.TicketCategories)
                .OrderByDescending(x => x.MatchDate)
                .ToList();
        }

        public List<Team> GetAllTeams()
        {
            return _context.Teams.AsNoTracking().ToList();
        }

        public List<Match> GetByDate(DateTime date)
        {
            return _context.Matches
                .Where(m => m.MatchDate.Date == date.Date)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                //.Include(m => m.TicketCategories)
                .ToList();
        }

        public Match? GetById(int id)
        {
            return _context.Matches
                .Include(x => x.HomeTeam)
                .ThenInclude(x => x.Players)
                .Include(x => x.AwayTeam)
                .ThenInclude(x => x.Players)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Match> GetByTeamName(string teamName)
        {
            return _context.Matches
                .Where(m => m.HomeTeam.Name == teamName || m.AwayTeam.Name == teamName)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                //.Include(m => m.TicketCategories)
                .ToList();
        }

        public Stadium? GetStadiumByTeamId(int teamId)
        {
            return _context.Stadiums.AsNoTracking().FirstOrDefault(x => x.TeamId == teamId);
        }

        public void Update(int id, Match newMatch)
        {
            var match = _context.Matches.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (match == null)
            {
                throw new KeyNotFoundException($"Match with ID {id} not found.");
            }
            match.MatchDate = newMatch.MatchDate;
            match.HomeTeamId = newMatch.HomeTeamId;
            match.AwayTeamId = newMatch.AwayTeamId;
            match.Location = newMatch.Location;
            match.StadiumName = newMatch.StadiumName;
            _context.Matches.Update(match);
            _context.SaveChanges();
        }
    }
}

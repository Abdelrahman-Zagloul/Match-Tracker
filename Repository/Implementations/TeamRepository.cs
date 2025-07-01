
using System;

namespace MatchTracker.Repository.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        public readonly AppDbContext _context;
        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Team team)
        {
            try
            {
                _context.Teams.Add(team);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the team.", ex);
            }
        }

        public bool Clear()
        {
            return _context.Teams.ExecuteDelete() > 0;
        }

        public bool Delete(int id)
        {
            var team = _context.Teams.Find(id);
            if (team == null)
                return false;
            _context.Teams.Remove(team);
            return _context.SaveChanges() > 0;
        }

        public List<Team> GetAll()
        {
            return _context.Teams
                .Include(t => t.Stadium)
                .Include(t => t.Players)
                .ToList();
        }

        public Team? GetById(int id)
        {
            return _context.Teams
                .Include(t => t.Stadium)
                .Include(t => t.Players)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Team> GetTeamsByCountry(string country)
        {
            return _context.Teams
                .Include(t => t.Stadium)
                .Include(t => t.Players)
                .Where(t => t.Country.ToLower() == country.ToLower())
                .ToList();
        }

        public Team? SearchByName(string teamName)
        {
            return _context.Teams
                .Where(t => t.Name.ToLower() == teamName.ToLower())
                .Include(t => t.Stadium)
                .Include(t => t.Players)
                .FirstOrDefault();
        }

        public void Update(int id, Team newTeam)
        {
            var existingTeam = _context.Teams.Find(id);
            if (existingTeam == null)
            {
                throw new Exception("Team not found.");
            }

            existingTeam.Name = newTeam.Name;
            existingTeam.CoachName = newTeam.CoachName;
            existingTeam.Country = newTeam.Country;
            existingTeam.FoundedYear = newTeam.FoundedYear;
            existingTeam.ChampionshipsWon = newTeam.ChampionshipsWon;
            existingTeam.LogoPath = newTeam.LogoPath;

            _context.Teams.Update(existingTeam);
            _context.SaveChanges();


        }

    }
}

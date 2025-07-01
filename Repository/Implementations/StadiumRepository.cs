

namespace MatchTracker.Repository.Implementations
{
    public class StadiumRepository : IStadiumRepository
    {
        public readonly AppDbContext _context;
        public StadiumRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Stadium stadium)
        {
            try
            {
                _context.Stadiums.Add(stadium);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the stadium.", ex);
            }
        }
        public void Update(int id, Stadium newStadium)
        {
            var existingStadium = _context.Stadiums.Find(id);

            if (existingStadium == null)
            {
                throw new Exception("Team not found.");
            }

            existingStadium.Name = newStadium.Name;
            existingStadium.Capacity = newStadium.Capacity;
            existingStadium.Location = newStadium.Location;
            existingStadium.TeamId = newStadium.TeamId;

            _context.SaveChanges();
        }
        public bool Delete(int id)
        {
            var Stadium = _context.Stadiums.Find(id);
            if (Stadium == null)
                return false;

            _context.Stadiums.Remove(Stadium);
            return _context.SaveChanges() > 0;
        }
        public bool Clear()
        {
            return _context.Stadiums.ExecuteDelete() > 0;
        }
        public List<Stadium> GetAll()
        {
            return _context.Stadiums
                .Include(s => s.Team)
                .ToList();
        }
        public Stadium? GetById(int id)
        {
            return _context.Stadiums
                .Include(s => s.Team)
                .FirstOrDefault(s => s.Id == id);
        }
        public List<Team> GetTeamsWithoutStadium()
        {
            var teams = _context.Teams
                .Include(t => t.Stadium)
                .Where(t => t.Stadium.TeamId == null)
                .ToList();
            return teams;
        }


    }
}



namespace MatchTracker.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public void Add(MatchForAddViewModel matchVM)
        {
            Match match = new Match
            {
                MatchDate = matchVM.MatchDate,
                Location = matchVM.Location ?? "",
                StadiumName = matchVM.StadiumName ?? "",
                AwayTeamId = matchVM.AwayTeamId,
                HomeTeamId = matchVM.HomeTeamId
            };
            try
            {
                _matchRepository.Add(match);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Match: {ex.Message}");
            }
        }

        public bool Clear()
        {
            try
            {
                return _matchRepository.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error clearing Matches: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return _matchRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Match with ID {id}: {ex.Message}");
            }
        }

        public List<Match> GetAll()
        {
            try
            {
                return _matchRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all Matches: {ex.Message}");
            }
        }

        public List<SelectListItem> GetTeams()
        {
            var SelectListItem = _matchRepository
                .GetAllTeams()
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

            return SelectListItem;
        }
        public List<Match> GetByDate(DateTime date)
        {
            try
            {
                return _matchRepository.GetByDate(date);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Matches by date {date}: {ex.Message}");
            }
        }

        public List<Match> GetByTeamName(string teamName)
        {
            try
            {
                return _matchRepository.GetByTeamName(teamName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Matches by team name '{teamName}': {ex.Message}");
            }
        }

        public void Update(int id, MatchForUpdateViewModel newMatch)
        {
            var match = new Match
            {
                Id = id,
                MatchDate = newMatch.MatchDate ?? DateTime.Now,
                Location = newMatch.Location ?? "",
                StadiumName = newMatch.StadiumName ?? "",
                AwayTeamId = newMatch.AwayTeamId ?? 0,
                HomeTeamId = newMatch.HomeTeamId ?? 0,
            };
            try
            {
                _matchRepository.Update(id, match);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error Updating Matches -- {ex.Message}");
            }
        }

        public Match? GetById(int id)
        {
            return _matchRepository.GetById(id);
        }

        public string GetStadiumNameByTeamId(int teamId)
        {
            return _matchRepository.GetStadiumByTeamId(teamId)?.Name ?? "Unknown Stadium";
        }

        public string GetStadiumLocationByTeamId(int teamId)
        {
            return _matchRepository.GetStadiumByTeamId(teamId)?.Location ?? "Unknown Location";
        }
    }
}

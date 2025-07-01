using MatchTracker.Models;

namespace MatchTracker.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamService(ITeamRepository teamRepository, IWebHostEnvironment webHostEnvironment)
        {
            _teamRepository = teamRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Add(TeamForAddViewModel teamVm)
        {
            Team team = new Team
            {
                Name = teamVm.Name,
                CoachName = teamVm.CoachName,
                Country = teamVm.Country,
                ChampionshipsWon = teamVm.ChampionshipsWon,
                FoundedYear = teamVm.FoundedYear,
                LogoPath = ConvertImage(FileSetting.TeamImagesFolderPath, teamVm.ImageFile)
            };
            try
            {
                _teamRepository.Add(team);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding team: {ex.Message}");
            }
        }
        public void Update(int id, TeamForUpdateViewModel teamVM)
        {
            string logoName = teamVM.LogoUrl;

            if (teamVM.ImageFile != null)
            {
                // Remove old logo
                string oldLogoPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSetting.TeamImagesFolderPath, logoName);
                if (File.Exists(oldLogoPath))
                {
                    File.Delete(oldLogoPath);
                }

                // Convert new logo
                logoName = ConvertImage(FileSetting.TeamImagesFolderPath, teamVM.ImageFile);
            }

            Team newTeam = new Team
            {
                Id = id,
                Name = teamVM.Name,
                CoachName = teamVM.CoachName,
                Country = teamVM.Country,
                FoundedYear = teamVM.FoundedYear ?? 0,
                ChampionshipsWon = teamVM.ChampionshipsWon ?? 0,
                LogoPath = logoName
            };

            try
            {
                _teamRepository.Update(id, newTeam);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating team: {ex.Message}");
            }
        }

        public bool Clear()
        {
            try
            {
                bool isDeleted = _teamRepository.Clear();
                if (isDeleted)
                {
                    // delete all images from wwwroot
                    string teamImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSetting.TeamImagesFolderPath);
                    if (Directory.Exists(teamImagesPath))
                    {
                        Directory.Delete(teamImagesPath, true);
                    }

                    // Recreate the directory
                    Directory.CreateDirectory(teamImagesPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Delete(int id)
        {
            string? LogoName = _teamRepository.GetById(id)?.LogoPath;
            bool isDeleted = _teamRepository.Delete(id);

            if (isDeleted)
            {
                string logoPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSetting.TeamImagesFolderPath, LogoName);
                if (File.Exists(logoPath))
                {
                    File.Delete(logoPath);
                }
            }

            return isDeleted;
        }

        public List<Team> GetAll()
        {
            return _teamRepository.GetAll();
        }

        public Team? GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID must be greater than zero.");
            }

            return _teamRepository.GetById(id);
        }

        public List<Team> GetTeamsByCountry(string country)
        {
            return _teamRepository.GetTeamsByCountry(country);
        }

        public Team? SearchByName(string searchTerm)
        {
            return _teamRepository.SearchByName(searchTerm);

        }

        private string ConvertImage(string teamFolderPath, IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, teamFolderPath, fileName);
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, teamFolderPath)))
            {
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, teamFolderPath));
            }
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}

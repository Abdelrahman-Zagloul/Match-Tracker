
namespace MatchTracker.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlayerService(IPlayerRepository playerRepository, IWebHostEnvironment webHostEnvironment)
        {
            _playerRepository = playerRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public void Add(PlayerForAddViewModel playerVM)
        {
            Player player = new Player
            {
                Name = playerVM.Name,
                Age = playerVM.Age,
                Nationality = playerVM.Nationality,
                Assists = playerVM.Assists,
                Number = playerVM.Number,
                MatchesPlayed = playerVM.MatchesPlayed,
                GoalsScored = playerVM.GoalsScored,
                Position = playerVM.Position,
                PicturePath = ConvertImage(FileSetting.PlayerImagesFolderPath, playerVM.ImageFile),
                TeamId = playerVM.TeamId,
            };
            try
            {
                _playerRepository.Add(player);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding player: {ex.Message}");
            }
        }

        public void Update(int id, PlayerForUpdateViewModel playerVM)
        {
            string pictureName = playerVM.PicturePath ?? "";
            if (playerVM.ImageFile != null)
            {
                RemoveImageFromServer(pictureName);
                // Convert new Picture
                pictureName = ConvertImage(FileSetting.PlayerImagesFolderPath, playerVM.ImageFile);
            }

            Player newPlayer = new Player
            {
                Id = id,
                Name = playerVM.Name ?? "",
                Nationality = playerVM.Nationality ?? "",
                Number = playerVM.Number ?? 0,
                Age = playerVM.Age ?? 0,
                MatchesPlayed = playerVM.MatchesPlayed ?? 0,
                GoalsScored = playerVM.GoalsScored ?? 0,
                Assists = playerVM.Assists ?? 0,
                Position = playerVM.Position,
                PicturePath = pictureName,
                TeamId = playerVM.TeamId
            };

            try
            {
                _playerRepository.Update(id, newPlayer);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Player: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            string? PictureName = _playerRepository.GetById(id)?.PicturePath ?? "";
            bool isDeleted = _playerRepository.Delete(id);
            if (isDeleted)
            {
                RemoveImageFromServer(PictureName);
            }
            return isDeleted;
        }

        public bool Clear()
        {
            try
            {
                bool isDeleted = _playerRepository.Clear();
                if (isDeleted)
                {
                    // delete all images from wwwroot
                    string playerImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, FileSetting.PlayerImagesFolderPath);
                    if (Directory.Exists(playerImagesPath))
                    {
                        Directory.Delete(playerImagesPath, true);
                    }

                    // Recreate the directory
                    Directory.CreateDirectory(playerImagesPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Player> GetAll()
        {
            try
            {
                return _playerRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving players: {ex.Message}");
            }
        }

        public Player? GetById(int id)
        {
            try
            {
                return _playerRepository.GetById(id);
            }
            catch
            {
                throw new Exception($"Player with ID {id} not found.");
            }
        }

        public List<SelectListItem> GetTeams()
        {
            var SelectListItem = _playerRepository
                .GetAllTeams()
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

            return SelectListItem;
        }

        public List<SelectListItem> GetPositions()
        {
            return Enum.GetValues(typeof(Position))
                .Cast<Position>()
                .Select(p => new SelectListItem
                {
                    Value = ((int)p).ToString(),
                    Text = p.ToString()
                }).ToList();
        }

        public Player? GetByName(string playerName)
        {
            try
            {
                return _playerRepository.GetByName(playerName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving players by name '{playerName}': {ex.Message}");
            }
        }

        public List<Player> GetByTeamName(string teamName)
        {
            try
            {
                return _playerRepository.GetByTeamName(teamName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving players by team name '{teamName}': {ex.Message}");
            }

        }

        public List<Player> GetByCountryName(string countryName)
        {
            try
            {
                return _playerRepository.GetByCountryName(countryName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving players by country name '{countryName}': {ex.Message}");
            }
        }
        public string GetPictureName(int id)
        {
            return _playerRepository.GetPictureName(id);
        }
        private string ConvertImage(string playerFolderPath, IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, playerFolderPath, fileName);
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, playerFolderPath)))
            {
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, playerFolderPath));
            }
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }

        private void RemoveImageFromServer(string imagePath)
        {
            // Remove old Picture
            string oldPicturePath = Path.Combine(_webHostEnvironment.WebRootPath, FileSetting.PlayerImagesFolderPath, imagePath);
            if (File.Exists(oldPicturePath))
            {
                File.Delete(oldPicturePath);
            }
        }

    }

}

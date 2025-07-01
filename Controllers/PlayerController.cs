namespace MatchTracker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var players = _playerService.GetAll();
            return View("Index", players);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var playerVM = new PlayerForAddViewModel
            {
                Teams = _playerService.GetTeams(),
                Positions = _playerService.GetPositions(),
            };
            return View("Add", playerVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var player = _playerService.GetById(id);
            if (player == null)
            {
                return View("NoPlayerFounded");
            }
            return View("Details", player);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult SaveAdd(PlayerForAddViewModel playerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _playerService.Add(playerVM);
                    return RedirectToAction("Index");
                }
                var VM = new PlayerForAddViewModel
                {
                    Teams = _playerService.GetTeams(),
                    Positions = _playerService.GetPositions(),
                };
                return View("Add", VM);
            }
            catch (Exception ex)
            {

                return View("HandelError", ex.Message);
            }

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var player = _playerService.GetById(id);
            if (player == null)
            {
                return View("NoPlayerFounded");
            }
            var playerVM = new PlayerForUpdateViewModel
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                MatchesPlayed = player.MatchesPlayed,
                GoalsScored = player.GoalsScored,
                Assists = player.Assists,
                Number = player.Number,
                Position = player.Position,
                PicturePath = player.PicturePath,
                TeamId = player.TeamId,
                Teams = _playerService.GetTeams(),
                Positions = _playerService.GetPositions()
            };
            return View("Update", playerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult SaveUpdate(int id, PlayerForUpdateViewModel newVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    newVM.PicturePath = _playerService.GetPictureName(id);
                    _playerService.Update(id, newVM);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                    return View("Update", newVM);
                }
            }
            newVM.Teams = _playerService.GetTeams();
            newVM.Positions = _playerService.GetPositions();
            return View("Update", newVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_playerService.Delete(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete the team.");
                    return View("Index", _playerService.GetAll());
                }
            }
            catch (Exception ex)
            {
                return View("HandelError", ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Clear()
        {
            try
            {
                if (_playerService.Clear())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to clear the players.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View("HandelError", ex.Message);
            }
        }


        [Authorize(Roles = "Admin,User")]
        public IActionResult SearchByName(string playerName)
        {
            var player = _playerService.GetByName(playerName);
            if (player == null)
            {
                return View("NoPlayerFounded");
            }
            return View("SearchByName", player);
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult SearchByCountry(string countryName)
        {
            var players = _playerService.GetByCountryName(countryName);
            if (!players.Any())
            {
                return View("NoPlayerFounded");
            }
            return View("SearchByCountry", players);
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult SearchByTeam(string teamName)
        {
            var players = _playerService.GetByTeamName(teamName);
            if (!players.Any())
            {
                return View("NoPlayerFounded");
            }
            return View("SearchByTeam", players);
        }

    }

}

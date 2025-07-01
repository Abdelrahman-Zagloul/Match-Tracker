
namespace MatchTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            var teams = _teamService.GetAll();
            return View("Index", teams);
        }

        public IActionResult GetById(int id)
        {
            var team = _teamService.GetById(id);
            if (team == null)
            {
                return View("NoTeamFounded");
            }
            return View("Details", team);
        }
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAdd(TeamForAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _teamService.Add(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                    return View("Add", model);
                }
            }

            return View("Add", model);

        }

        public IActionResult Delete(int id)
        {
            try
            {
                if (_teamService.Delete(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete the team.");
                    return View("Index", _teamService.GetAll());
                }
            }
            catch (Exception ex)
            {
                return View("HandelError", ex.Message);
            }
        }

        public IActionResult Clear()
        {
            if (_teamService.Clear())
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to clear the teams.");
                return View("Index", _teamService.GetAll());
            }
        }


        public IActionResult Update(int id)
        {
            var team = _teamService.GetById(id);
            if (team == null)
            {
                return View("NoTeamFounded");
            }

            var newVM = new TeamForUpdateViewModel
            {
                Id = team.Id,
                Name = team?.Name ?? string.Empty,
                CoachName = team?.CoachName ?? string.Empty,
                Country = team?.Country ?? string.Empty,
                FoundedYear = team?.FoundedYear ?? 0,
                ChampionshipsWon = team?.ChampionshipsWon ?? 0,
                LogoUrl = team?.LogoPath
            };

            return View("Update", newVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUpdate(int id, TeamForUpdateViewModel newVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    newVM.LogoUrl = _teamService.GetById(id)?.LogoPath;
                    _teamService.Update(id, newVM);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                    return View("Update", newVM);
                }
            }
            return View("Update", newVM);
        }

        [AllowAnonymous]
        public IActionResult SearchByName(string teamName)
        {
            var team = _teamService.SearchByName(teamName);
            if (team == null)
            {
                ModelState.AddModelError("", "No team found with that name.");
                return View("NoTeamFounded");
            }
            return View("Details", team);
        }
        public IActionResult SearchByCountry(string countryName)
        {
            try
            {
                var teams = _teamService.GetTeamsByCountry(countryName);
                return View("SearchByCountry", teams);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while searching by country: {ex.Message}");
                return RedirectToAction("Index");


            }
        }
    }
}

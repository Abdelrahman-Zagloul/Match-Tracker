namespace MatchTracker.Controllers
{

    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;
        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var matches = _matchService.GetAll();
            return View("Index", matches);
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var match = _matchService.GetById(id);
            if (match == null)
            {
                return View("NoMatchFounded");
            }
            // make cookie for return
            Response.Cookies.Append("MatchId", id.ToString());
            return View("Details", match);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var matchVM = new MatchForAddViewModel
            {
                Teams = _matchService.GetTeams(),
            };
            return View("Add", matchVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult SaveAdd(MatchForAddViewModel matchVM)
        {
            if (ModelState.IsValid)
            {
                matchVM.Location = _matchService.GetStadiumLocationByTeamId(matchVM.HomeTeamId);
                matchVM.StadiumName = _matchService.GetStadiumNameByTeamId(matchVM.HomeTeamId);
                _matchService.Add(matchVM);
                return RedirectToAction("Index");
            }
            matchVM.Teams = _matchService.GetTeams();
            return View("Add", matchVM);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var Team = _matchService.GetById(id);
            if (Team == null)
            {
                return View("NoMatchFounded");
            }
            var matchVM = new MatchForUpdateViewModel
            {
                Id = Team.Id,
                MatchDate = Team.MatchDate,
                AwayTeamId = Team.AwayTeam.Id,
                HomeTeamId = Team.HomeTeam.Id,
                Teams = _matchService.GetTeams(),
            };
            return View("Update", matchVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult SaveUpdate(int id, MatchForUpdateViewModel matchVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    matchVM.Location = _matchService.GetStadiumLocationByTeamId(matchVM.HomeTeamId ?? 0);
                    matchVM.StadiumName = _matchService.GetStadiumNameByTeamId(matchVM.HomeTeamId ?? 0);
                    _matchService.Update(id, matchVM);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View("Handel Error");
                }
            }
            matchVM.Teams = _matchService.GetTeams();
            return View("Update", matchVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _matchService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Handel Error");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Clear()
        {
            try
            {
                _matchService.Clear();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Handel Error");
            }
        }

        [AllowAnonymous]
        public IActionResult GetByDate(DateTime date)
        {
            var matches = _matchService.GetByDate(date);
            return View("Index", matches);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult ValidateTeams(int HomeTeamId, int AwayTeamId)
        {
            if (HomeTeamId == AwayTeamId)
            {
                return Json("Home and Away teams must be different.");
            }

            return Json(true);
        }
    }
}
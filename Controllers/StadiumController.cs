namespace MatchTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StadiumController : Controller
    {
        private readonly IStadiumService _stadiumService;
        public StadiumController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }
        public IActionResult Index()
        {
            var teams = _stadiumService.GetAll();
            return View("Index", teams);
        }
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            var stadiumFromDB = _stadiumService.GetById(id);

            if (stadiumFromDB == null)
            {
                return NotFound();
            }
            return View("Details", stadiumFromDB);
        }

        public IActionResult Add()
        {
            var model = new StadiumForAddViewModel
            {
                Teams = _stadiumService.GetTeamsWithoutStadiums()
            };
            return View("Add",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAdd(StadiumForAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _stadiumService.Add(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                    model.Teams = _stadiumService.GetTeamsWithoutStadiums();
                    return View("Add", model);
                }
            }

            return View("Add", model);

        }
        public IActionResult Delete(int id)
        {
            try
            {
                if (_stadiumService.Delete(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete the team.");
                    return View("Index", _stadiumService.GetAll());
                }
            }
            catch (Exception ex)
            {
                return View("HandelError", ex.Message);
            }
        }

        public IActionResult Clear()
        {
            if (_stadiumService.Clear())
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to clear the Stadiums.");
                return View("Index", _stadiumService.GetAll());
            }
        }


        public IActionResult Update(int id)
        {
            var team = _stadiumService.GetById(id);
            if (team == null)
            {
                return NotFound();
            }
            var newVM = new StadiumForUpdateViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Location = team.Location,
                Capacity = team.Capacity,
                TeamId = team.TeamId,
                Teams = _stadiumService.GetTeamsWithoutStadiums()
            };
            return View("Update", newVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUpdate(int id, StadiumForUpdateViewModel newVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   _stadiumService.Update(id, newVM);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View("HandelError", ex.Message);
                }
            }
            newVM.Teams = _stadiumService.GetTeamsWithoutStadiums();
            return View("Update", newVM);
        }
    }
}

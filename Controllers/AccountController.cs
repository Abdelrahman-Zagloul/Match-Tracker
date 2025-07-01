namespace MatchTracker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRegister(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isFirstUser = !_userManager.Users.Any();

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ImagePath = FileSetting.DefaultImagePath,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (isFirstUser)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Match");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View("Register", model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View("Login", model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Dashboard", "User");
                    }

                    return RedirectToAction("Index", "Match");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                }
            }
            return View("Login", model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Match");
        }

        public IActionResult LoginFirst()
        {
            return View("LoginFirst");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }


        public async Task<IActionResult> IsUserNameInUse(string UserName, string Id)
        {

            var user = await _userManager.FindByNameAsync(UserName);

            if (user == null)
                return Json(true);

            if (user.Id == Id)
                return Json(true);

            return Json(false);


        }
        public IActionResult IsEmailInUse(string Email)
        {

            var user = _userManager.FindByEmailAsync(Email);
            if (user.Result != null)
            {
                return Json(false);
            }
            return Json(true);


        }

    }
}

namespace MatchTracker.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View("Dashboard");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault() ?? "User";
            var model = new ProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                ImageUrl = user.ImagePath ?? FileSetting.DefaultImagePath,
                Role = Enum.TryParse<Role>(role, out var parsedRole) ? parsedRole : Role.User,
            };

            return View("Profile", model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var addUserViewModel = new AccountForAddViewModel
            {

                Roles = GetSelectListItemsOfRole()
            };
            return View("Add", addUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAdd(AccountForAddViewModel model)
        {
            if (ModelState.IsValid)
            {
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
                    // Assign role to the user
                    var assignToRole = await _userManager.AddToRoleAsync(user, model.Role.ToString());
                    if (assignToRole.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in assignToRole.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            model.Roles = GetSelectListItemsOfRole();
            return View("AddUser", model);
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return Content("NotFount");

            var account = new AccountForUpdateViewModel
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                ImagePath = user.ImagePath,
                PhoneNumber = user.PhoneNumber!,
                Role = Enum.Parse<Role>(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value!),
            };
            return View("Update", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUpdate(AccountForUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email!);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View("Update", model);
                }

                if (model.ImageFile != null && user.ImagePath != "Default.webp")
                {
                    DeleteOldImage(FileSetting.UserImagesFolderPath, user.ImagePath);
                    user.ImagePath = ConvertImage(FileSetting.UserImagesFolderPath, model.ImageFile!);
                }

                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Update", model);
        }

        public IActionResult ChangePassword(string id)
        {
            var user = _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View("ChangePassword");
            }

            var account = new AccountForChangePassword { Id = id, Email = user.Result?.Email };
            return View("ChangePassword", account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveChangePassword(AccountForChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id!);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View("ChangePassword", model);
                }
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {

                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("ChangePassword", model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var allUsers = _userManager.Users.ToList();

            var userList = new List<AccountForUpdateViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new AccountForUpdateViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    ImagePath = user.ImagePath,
                    Role = Enum.Parse<Role>(roles.FirstOrDefault()!),
                });
            }
            return View("GetAllUser", userList);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View("GetAllUser");
            }

            if (_userManager.GetUserId(User) == id)
            {
                TempData["Error"] = "You cannot delete your own account.";
                return RedirectToAction("GetAllUser");
            }

            if (user.ImagePath != FileSetting.DefaultImagePath)
            {
                DeleteOldImage(FileSetting.UserImagesFolderPath, user.ImagePath);
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "Deleted Successfully";
                return RedirectToAction("GetAllUser");
            }
            TempData["Error"] = "Error in Deleted";
            return View("GetAllUser");
        }

        // Remote validation methods
        public IActionResult CheckImageFormat(IFormFile ImageFile)
        {
            var allowedExtensions = FileSetting.AllowedExtensions.Split(',');
            var fileExtension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(false);
            }
            return Json(true); 
        }
        public async Task<IActionResult> CheckPassword(string oldPassword, string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return Json(false);

            bool isTruePassword = await _userManager.CheckPasswordAsync(user, oldPassword);
            return Json(isTruePassword);
        }
        public IActionResult CheckNewPassword(string newPassword, string oldPassword)
        {
            bool isMatch = newPassword == oldPassword;
            return Json(!isMatch);

        }
        private List<SelectListItem> GetSelectListItemsOfRole()
        {
            return Enum.GetValues<Role>()
              .Select(r => new SelectListItem
              {
                  Value = r.ToString(),
                  Text = r.ToString()
              }).ToList();
        }
        private string ConvertImage(string userFolderPath, IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}_{file.FileName}";
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, userFolderPath, fileName);
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, userFolderPath)))
            {
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, userFolderPath));
            }
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
        private void DeleteOldImage(string userFolderPath, string imageUrl)
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, userFolderPath, imageUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

    }
}
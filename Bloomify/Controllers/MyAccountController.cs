using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bloomify.Models;
using System.Threading.Tasks;
using Bloomify.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Bloomify.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IAccountService _authService;
        private readonly ILogger<MyAccountController> _logger;

        public MyAccountController(IAccountService authService, ILogger<MyAccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var (success, message) = await _authService.LoginAsync(model.Email, model.Password, model.RememberMe);
                if (success)
                {
                    _logger.LogInformation("User logged in successfully.");
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, message);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new BloomifyUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name
                };

                var (success, message) = await _authService.RegisterAsync(user, model.Password);
                if (success)
                {
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, message);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

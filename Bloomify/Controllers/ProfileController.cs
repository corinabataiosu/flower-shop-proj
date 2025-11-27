using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bloomify.Models;
using Bloomify.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace Bloomify.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountService _authService;
        private readonly UserManager<BloomifyUser> _userManager;
        private readonly ILogger<ProfileController> _logger;
        private readonly IOrderService _orderService;

        public ProfileController(
            IAccountService authService,
            UserManager<BloomifyUser> userManager,
            ILogger<ProfileController> logger,
            IOrderService orderService)
        {
            _authService = authService;
            _userManager = userManager;
            _logger = logger;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var orders = _orderService.GetOrdersByUserID(user.Id);
            ViewBag.Orders = orders;

            var roles = await _userManager.GetRolesAsync(user);
            bool isAdmin = roles.Contains("Admin");
            ViewBag.IsAdmin = isAdmin;

            if (isAdmin)
            {
                var allOrders = _orderService.GetAllOrders();
                ViewBag.AllOrders = allOrders;
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,PhoneNumber,Email,Address")] BloomifyUser model, IFormFile? profileImage)
        {
            _logger.LogInformation("Starting profile edit for user");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User not found");
                return NotFound();
            }

            // Ensure Email is not changed
            model.Email = user.Email;

            // Validate image type if uploaded
            if (profileImage != null && profileImage.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var ext = Path.GetExtension(profileImage.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("profileImage", "Only .jpg, .jpeg, .png, .gif, .webp files are allowed.");
                }
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                // Ensure Email is present in the form for redisplay
                model.Email = user.Email;
                return View(model);
            }

            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            // Handle profile image upload
            if (profileImage != null && profileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                var fileName = $"user_{user.Id}_profile{Path.GetExtension(profileImage.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }
                user.ProfileImagePath = $"/images/profiles/{fileName}";
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            // Ensure Email is present in the form for redisplay
            model.Email = user.Email;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The new password and confirmation password do not match.");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Password changed successfully.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsReceived(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.MarkOrderAsReceived(orderId);
            TempData["SuccessMessage"] = "Order marked as received successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}

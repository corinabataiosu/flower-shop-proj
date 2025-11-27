using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bloomify.Models;
using Bloomify.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<BloomifyUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            UserManager<BloomifyUser> userManager,
            IOrderService orderService,
            ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userViewModels = new List<UserViewModel>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userViewModels.Add(new UserViewModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        Roles = roles.ToList()
                    });
                }

                return View(userViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users");
                TempData["ErrorMessage"] = "An error occurred while fetching users.";
                return View(new List<UserViewModel>());
            }
        }

        public IActionResult AllOrders()
        {
            try
            {
                var orders = _orderService.GetAllOrders();
                return View(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching orders");
                TempData["ErrorMessage"] = "An error occurred while fetching orders.";
                return View(new List<Order>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserRole(string userId, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }

                var isInRole = await _userManager.IsInRoleAsync(user, role);
                if (isInRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                    TempData["SuccessMessage"] = $"Removed {role} role from user.";
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, role);
                    TempData["SuccessMessage"] = $"Added {role} role to user.";
                }

                return RedirectToAction(nameof(Users));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while toggling user role");
                TempData["ErrorMessage"] = "An error occurred while updating user role.";
                return RedirectToAction(nameof(Users));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                var order = _orderService.GetOrderById(orderId);
                if (order == null)
                {
                    return NotFound();
                }

                order.Status = status;
                _orderService.UpdateOrder(order);
                TempData["SuccessMessage"] = "Order status updated successfully.";
                return RedirectToAction(nameof(AllOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order status");
                TempData["ErrorMessage"] = "An error occurred while updating order status.";
                return RedirectToAction(nameof(AllOrders));
            }
        }
    }

    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }
    }
}

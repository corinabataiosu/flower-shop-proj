using Bloomify.Models;
using Bloomify.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Bloomify.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<BloomifyUser> _userManager;
        private readonly SignInManager<BloomifyUser> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public AccountService(
            UserManager<BloomifyUser> userManager,
            SignInManager<BloomifyUser> signInManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        public async Task<(bool success, string message)> RegisterAsync(BloomifyUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return (true, "Registration successful.");
            }

            return (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<(bool success, string message)> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "Invalid login attempt.");
            }

            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return (true, "Login successful.");
            }

            if (result.IsLockedOut)
            {
                return (false, "Account locked out. Please try again later.");
            }

            return (false, "Invalid login attempt.");
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsEmailConfirmedAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null && await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<(bool success, string message)> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return (result.Succeeded, result.Succeeded ? "Email confirmed successfully." : string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<(bool success, string message)> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return (true, "Password reset token generated successfully.");
        }

        public async Task<(bool success, string message)> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "User not found.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return (result.Succeeded, result.Succeeded ? "Password has been reset successfully." : string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
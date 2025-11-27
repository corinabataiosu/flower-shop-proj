using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IAccountService
    {
        Task<(bool success, string message)> RegisterAsync(BloomifyUser user, string password);
        Task<(bool success, string message)> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task<bool> IsEmailConfirmedAsync(string email);
        Task<(bool success, string message)> ConfirmEmailAsync(string userId, string token);
        Task<(bool success, string message)> ForgotPasswordAsync(string email);
        Task<(bool success, string message)> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetCurrentUser();
        User GetUserById(int id);
        void CreateUser(User user);
        void DeleteUser(int id);
    }
}

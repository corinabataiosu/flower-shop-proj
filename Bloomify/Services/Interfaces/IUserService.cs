using Bloomify.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Bloomify.Services.Interfaces
{
    public interface IUserService
    {
        IdentityResult CreateUser(BloomifyUser user, string password);
        BloomifyUser GetUserByEmail(string email);
        BloomifyUser GetUserById(int id);
        IEnumerable<BloomifyUser> GetAllUsers();
        IdentityResult UpdateUser(BloomifyUser user);
        IdentityResult DeleteUser(BloomifyUser user);
        IdentityResult AddToRole(BloomifyUser user, string role);
        IdentityResult RemoveFromRole(BloomifyUser user, string role);
        IList<string> GetUserRoles(BloomifyUser user);
        BloomifyUser GetCurrentUser();
    }
}

using Bloomify.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Bloomify.Repositories.Interfaces
{
    public interface IUserRepository
    {
        BloomifyUser GetUserByEmail(string email);
        BloomifyUser GetUserById(int id);
        IEnumerable<BloomifyUser> GetAllUsers();
        IdentityResult CreateUser(BloomifyUser user, string password);
        IdentityResult UpdateUser(BloomifyUser user);
        IdentityResult DeleteUser(BloomifyUser user);
        IdentityResult AddToRole(BloomifyUser user, string role);
        IdentityResult RemoveFromRole(BloomifyUser user, string role);
        IList<string> GetUserRoles(BloomifyUser user);
    }
}

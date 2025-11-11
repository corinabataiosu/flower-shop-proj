using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bloomify.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<BloomifyUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserRepository(UserManager<BloomifyUser> userManager,
                              RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public BloomifyUser GetUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
        }

        public BloomifyUser GetUserById(int id)
        {
            return _userManager.FindByIdAsync(id.ToString()).GetAwaiter().GetResult();
        }

        public IEnumerable<BloomifyUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public IdentityResult CreateUser(BloomifyUser user, string password)
        {
            return _userManager.CreateAsync(user, password).GetAwaiter().GetResult();
        }

        public IdentityResult UpdateUser(BloomifyUser user)
        {
            return _userManager.UpdateAsync(user).GetAwaiter().GetResult();
        }

        public IdentityResult DeleteUser(BloomifyUser user)
        {
            return _userManager.DeleteAsync(user).GetAwaiter().GetResult();
        }

        public IdentityResult AddToRole(BloomifyUser user, string role)
        {
            return _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
        }

        public IdentityResult RemoveFromRole(BloomifyUser user, string role)
        {
            return _userManager.RemoveFromRoleAsync(user, role).GetAwaiter().GetResult();
        }

        public IList<string> GetUserRoles(BloomifyUser user)
        {
            return _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
        }
    }
}

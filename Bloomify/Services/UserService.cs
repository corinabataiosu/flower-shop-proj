using System.Security.Claims;
using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Bloomify.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IRepositoryWrapper repositoryWrapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IdentityResult CreateUser(BloomifyUser user, string password)
        {
            return _userRepository.CreateUser(user, password);
        }

        public BloomifyUser GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public BloomifyUser GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<BloomifyUser> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public IdentityResult UpdateUser(BloomifyUser user)
        {
            return _userRepository.UpdateUser(user);
        }

        public IdentityResult DeleteUser(BloomifyUser user)
        {
            return _userRepository.DeleteUser(user);
        }

        public IdentityResult AddToRole(BloomifyUser user, string role)
        {
            return _userRepository.AddToRole(user, role);
        }

        public IdentityResult RemoveFromRole(BloomifyUser user, string role)
        {
            return _userRepository.RemoveFromRole(user, role);
        }

        public IList<string> GetUserRoles(BloomifyUser user)
        {
            return _userRepository.GetUserRoles(user);
        }

        public BloomifyUser GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return _userRepository.GetUserById(int.Parse(userId));
        }
    }
}

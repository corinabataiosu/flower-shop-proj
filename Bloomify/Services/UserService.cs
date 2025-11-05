using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Services.Interfaces;

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
        public void CreateUser(User user)
        {
            _repositoryWrapper.UserRepository.Create(user);
            _repositoryWrapper.Save();
        }

        public void DeleteUser(int id)
        {
            var user = _repositoryWrapper.UserRepository.FindByCondition(u => u.UserID == id).FirstOrDefault();
            if (user != null)
            {
                _repositoryWrapper.UserRepository.Delete(user);
                _repositoryWrapper.Save();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repositoryWrapper.UserRepository.FindAll().ToList();
        }

        public User GetUserById(int id)
        {
            return _repositoryWrapper.UserRepository
                .FindByCondition(u => u.UserID == id)
                .FirstOrDefault();
        }

        public User? GetCurrentUser()
        {
            return _userRepository.GetUserByID(1);
        }
    }
}

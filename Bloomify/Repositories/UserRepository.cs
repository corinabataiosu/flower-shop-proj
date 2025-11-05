using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class UserRepository :  RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email);
        }
        public void AddUser(User user)
        {
            Create(user);
        }
        public void UpdateUser(User user)
        {
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public User GetUserByID(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserID == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
    }
}

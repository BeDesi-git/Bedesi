using BeDesi.Core.Models;

namespace BeDesi.Core.Repository.Contracts
{
    public interface IUserRepository
    {
        public Task<int> Register(User user);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(int id);
        public Task<bool> UpdatePassword(int id, string salt, string passwordHash);
        public Task<bool> Update(User user);
    }
}

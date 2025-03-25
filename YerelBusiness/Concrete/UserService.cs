using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users.ToList();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            return await _userRepository.GetByRoleAsync(role);
        }

        public async Task<User> AddUser(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }
    }
}
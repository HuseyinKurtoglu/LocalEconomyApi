using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetUsersByRole(string role);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(string id);
    }
}
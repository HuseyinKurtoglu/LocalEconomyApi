using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;
using System.Collections.Generic;

namespace LocalEconomyApi.Abstract
{

    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
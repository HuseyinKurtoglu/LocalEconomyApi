using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;
using System.Collections.Generic;

namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}

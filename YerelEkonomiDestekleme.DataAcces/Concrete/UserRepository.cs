using LocalEconomyApi.Data;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LocalEconomyApi.DataAccess.Concrete.EntityFramework
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Where(u => !u.IsDeleted).ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
        }

        public void Add(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.IsDeleted = false;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            user.UpdatedDate = DateTime.Now;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            user.IsDeleted = true;
            Update(user);
        }
    }
}

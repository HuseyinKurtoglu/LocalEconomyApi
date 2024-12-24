using LocalEconomyApi.Data;
using LocalEconomyApi.DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;
using LocalEconomyApi.Models;

namespace LocalEconomyApi.DataAccess.Concrete
{
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        private readonly AppDbContext _context;

        public BusinessRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Business> GetBusinessesByCity(string city)
        {
            return _context.Businesses.Where(b => b.City == city).ToList();
        }
    }
}

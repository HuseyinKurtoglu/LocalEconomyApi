
using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;

namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface IBusinessRepository : IGenericRepository<Business>
    {
        // Business'a özgü işlemler (isteğe bağlı)
        IEnumerable<Business> GetBusinessesByCity(string city);
    }
}

using LocalEconomyApi.Models;
using System.Collections.Generic;
using LocalEconomyApi.Models.Concrete;

namespace LocalEconomyApi.Abstract.business
{
    public interface IBusinessService
    {
        IEnumerable<Business> GetAllBusinesses();
        Business GetBusinessById(int id);
        void AddBusiness(Business business);
        void UpdateBusiness(Business business);
        void DeleteBusiness(int id);
        IEnumerable<Business> GetBusinessesByCity(string city);
    }
}

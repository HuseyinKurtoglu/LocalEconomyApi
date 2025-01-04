using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;
using System.Collections.Generic;

namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Business> GetBusinessesByCategoryId(int categoryId);
        IEnumerable<Campaign> GetCampaignsByCategoryId(int categoryId);
        IEnumerable<Category> GetActiveCategories();
        Category FindCategoryByName(string name);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllWithBusinessesAsync();
        Task<Category?> GetByIdWithBusinessesAsync(int id);
        Task<List<BusinessEntity>> GetBusinessesByCategoryAsync(int categoryId);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<List<Category>> GetActiveCategoriesAsync();
        Task<Category?> FindCategoryByNameAsync(string name);
    }
}

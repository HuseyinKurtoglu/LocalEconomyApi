using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelBusiness.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<List<Category>> GetActiveCategoriesAsync();
        Task<Category> FindCategoryByNameAsync(string name);
    }
}

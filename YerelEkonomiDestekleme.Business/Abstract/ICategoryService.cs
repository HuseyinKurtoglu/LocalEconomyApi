using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
} 
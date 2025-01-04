using LocalEconomyApi.Models;
using System.Collections.Generic;

namespace LocalEconomyApi.Abstract
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        IEnumerable<Category> GetActiveCategories();
        Category FindCategoryByName(string name);
    }
}

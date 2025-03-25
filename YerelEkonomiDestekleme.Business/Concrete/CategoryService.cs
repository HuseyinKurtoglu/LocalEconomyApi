using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddCategory(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
            }
        }
    }
} 
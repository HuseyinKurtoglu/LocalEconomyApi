using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelBusiness.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelBusiness.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.ToList();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepository.GetCategoryByNameAsync(name);
        }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            return await _categoryRepository.GetActiveCategoriesAsync();
        }

        public async Task<Category> FindCategoryByNameAsync(string name)
        {
            return await _categoryRepository.FindCategoryByNameAsync(name);
        }
    }
}
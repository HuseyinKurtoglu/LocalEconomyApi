using LocalEconomyApi.Abstract;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using System;
using System.Collections.Generic;

namespace LocalEconomyApi.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            var category = _categoryRepository.Get(c => c.CategoryId == id);
            if (category == null)
                throw new KeyNotFoundException($"ID: {id} olan kategori bulunamadı.");

            return category;
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _categoryRepository.Get(c => c.CategoryId == category.CategoryId);
            if (existingCategory == null)
                throw new KeyNotFoundException($"ID: {category.CategoryId} olan kategori bulunamadı.");

            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.Get(c => c.CategoryId == id);
            if (category == null)
                throw new KeyNotFoundException($"ID: {id} olan kategori bulunamadı.");

            _categoryRepository.Delete(category);
        }

        public IEnumerable<Category> GetActiveCategories()
        {
            return _categoryRepository.GetActiveCategories();
        }

        public Category FindCategoryByName(string name)
        {
            return _categoryRepository.FindCategoryByName(name);
        }
    }
}
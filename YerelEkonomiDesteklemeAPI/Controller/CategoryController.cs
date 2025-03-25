using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;
using YerelEkonomiDestekleme.Business.Abstract;
using System.Linq;

namespace YerelEkonomiDesteklemeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            var optimizedCategories = categories.Select(c => new
            {
                c.CategoryId,
                c.Name,
                c.Description,
                Businesses = c.Businesses?.Select(b => new
                {
                    b.BusinessId,
                    b.Name,
                    b.Description,
                    b.City
                }).ToList(),
                Campaigns = c.Campaigns?.Select(c => new
                {
                    c.CampaignId,
                    c.Title,
                    c.Description,
                    c.DiscountRate,
                    c.StartDate,
                    c.EndDate,
                    c.BusinessId
                }).ToList(),
                c.IsDeleted,
                c.CreatedDate
            }).ToList();
            return Ok(optimizedCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            var optimizedCategory = new
            {
                category.CategoryId,
                category.Name,
                category.Description,
                Businesses = category.Businesses?.Select(b => new
                {
                    b.BusinessId,
                    b.Name,
                    b.Description,
                    b.City
                }).ToList(),
                Campaigns = category.Campaigns?.Select(c => new
                {
                    c.CampaignId,
                    c.Title,
                    c.Description,
                    c.DiscountRate,
                    c.StartDate,
                    c.EndDate,
                    c.BusinessId
                }).ToList(),
                category.IsDeleted,
                category.CreatedDate
            };
            return Ok(optimizedCategory);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var createdCategory = await _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            var updatedCategory = await _categoryService.UpdateCategory(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
} 
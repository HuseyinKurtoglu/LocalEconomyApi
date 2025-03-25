using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Concrete;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YerelEkonomiDesteklemeAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly AppDbContext _context;

        public BusinessController(IBusinessService businessService, AppDbContext context)
        {
            _businessService = businessService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessEntity>>> GetAll()
        {
            var businesses = await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => !b.IsDeleted)
                .ToListAsync();

            var result = businesses.Select(b => new
            {
                b.BusinessId,
                b.Name,
                b.Description,
                b.Address,
                b.City,
                b.Phone,
                b.Email,
                b.ImageUrl,
                b.CategoryId,
                Category = b.Category != null ? new { b.Category.CategoryId, b.Category.Name } : null,
                Campaigns = b.Campaigns?.Where(c => !c.IsDeleted).Select(c => new
                {
                    c.CampaignId,
                    c.Title,
                    c.Description,
                    c.DiscountRate,
                    c.StartDate,
                    c.EndDate,
                    c.CategoryId
                })
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessEntity>> GetById(int id)
        {
            var business = await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .FirstOrDefaultAsync(b => b.BusinessId == id && !b.IsDeleted);

            if (business == null)
            {
                return NotFound();
            }

            var result = new
            {
                business.BusinessId,
                business.Name,
                business.Description,
                business.Address,
                business.City,
                business.Phone,
                business.Email,
                business.ImageUrl,
                business.CategoryId,
                Category = business.Category != null ? new { business.Category.CategoryId, business.Category.Name } : null,
                Campaigns = business.Campaigns?.Where(c => !c.IsDeleted).Select(c => new
                {
                    c.CampaignId,
                    c.Title,
                    c.Description,
                    c.DiscountRate,
                    c.StartDate,
                    c.EndDate,
                    c.CategoryId
                })
            };

            return Ok(result);
        }

        [HttpGet("city/{city}")]
        public async Task<ActionResult<IEnumerable<BusinessEntity>>> GetByCity(string city)
        {
            var businesses = await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => b.City == city && !b.IsDeleted)
                .ToListAsync();

            var result = businesses.Select(b => new
            {
                b.BusinessId,
                b.Name,
                b.Description,
                b.Address,
                b.City,
                b.Phone,
                b.Email,
                b.ImageUrl,
                b.CategoryId,
                Category = b.Category != null ? new { b.Category.CategoryId, b.Category.Name } : null,
                Campaigns = b.Campaigns?.Where(c => !c.IsDeleted).Select(c => new
                {
                    c.CampaignId,
                    c.Title,
                    c.Description,
                    c.DiscountRate,
                    c.StartDate,
                    c.EndDate,
                    c.CategoryId
                })
            });

            return Ok(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BusinessEntity>>> GetByCategory(int categoryId)
        {
            var businesses = await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => b.CategoryId == categoryId && !b.IsDeleted)
                .ToListAsync();

            var result = businesses.Select(b => new
            {
                b.BusinessId,
                b.Name,
                b.Description,
                b.Address,
                b.City,
                b.Phone,
                b.Email,
                b.ImageUrl,
                b.CategoryId,
                Category = b.Category != null ? new { b.Category.CategoryId, b.Category.Name } : null,
                Campaigns = b.Campaigns?.Where(c => !c.IsDeleted).Select(c => new
                {
                    c.CampaignId,
                    c.Title,
                    c.Description,
                    c.DiscountRate,
                    c.StartDate,
                    c.EndDate,
                    c.CategoryId
                })
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BusinessEntity>> Create([FromBody] BusinessEntity business)
        {
            var createdBusiness = await _businessService.AddBusiness(business);
            return CreatedAtAction(nameof(GetById), new { id = createdBusiness.BusinessId }, createdBusiness);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BusinessEntity business)
        {
            if (id != business.BusinessId)
            {
                return BadRequest();
            }
            var updatedBusiness = await _businessService.UpdateBusiness(business);
            if (updatedBusiness == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var business = await _businessService.GetBusinessById(id);
            if (business == null)
            {
                return NotFound();
            }
            await _businessService.DeleteBusiness(id);
            return NoContent();
        }

        [HttpPut("{id}/soft-delete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var result = await _businessService.SoftDeleteBusiness(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

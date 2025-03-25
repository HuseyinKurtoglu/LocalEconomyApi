using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;
using YerelEkonomiDestekleme.Business.Abstract;
using System.Linq;
using System;

namespace YerelEkonomiDesteklemeAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Campaign>>> GetAll()
        {
            var campaigns = await _campaignService.GetAllCampaigns();
            var optimizedCampaigns = campaigns.Select(c => new
            {
                c.CampaignId,
                c.Title,
                c.Description,
                c.DiscountRate,
                c.StartDate,
                c.EndDate,
                c.BusinessId,
                c.CategoryId,
                Business = c.Business != null ? new
                {
                    c.Business.BusinessId,
                    c.Business.Name,
                    c.Business.Description,
                    c.Business.City
                } : null,
                Category = c.Category != null ? new
                {
                    c.Category.CategoryId,
                    c.Category.Name,
                    c.Category.Description
                } : null,
                c.IsDeleted,
                c.CreatedDate
            }).ToList();
            return Ok(optimizedCampaigns);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetById(int id)
        {
            var campaign = await _campaignService.GetCampaignById(id);
            if (campaign == null)
            {
                return NotFound();
            }
            var optimizedCampaign = new
            {
                campaign.CampaignId,
                campaign.Title,
                campaign.Description,
                campaign.DiscountRate,
                campaign.StartDate,
                campaign.EndDate,
                campaign.BusinessId,
                campaign.CategoryId,
                Business = campaign.Business != null ? new
                {
                    campaign.Business.BusinessId,
                    campaign.Business.Name,
                    campaign.Business.Description,
                    campaign.Business.City
                } : null,
                Category = campaign.Category != null ? new
                {
                    campaign.Category.CategoryId,
                    campaign.Category.Name,
                    campaign.Category.Description
                } : null,
                campaign.IsDeleted,
                campaign.CreatedDate
            };
            return Ok(optimizedCampaign);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<Campaign>>> GetByCategory(int categoryId)
        {
            var campaigns = await _campaignService.GetCampaignsByCategory(categoryId);
            var optimizedCampaigns = campaigns.Select(c => new
            {
                c.CampaignId,
                c.Title,
                c.Description,
                c.DiscountRate,
                c.StartDate,
                c.EndDate,
                c.BusinessId,
                c.CategoryId,
                Business = c.Business != null ? new
                {
                    c.Business.BusinessId,
                    c.Business.Name,
                    c.Business.Description,
                    c.Business.City
                } : null,
                Category = c.Category != null ? new
                {
                    c.Category.CategoryId,
                    c.Category.Name,
                    c.Category.Description
                } : null,
                c.IsDeleted,
                c.CreatedDate
            }).ToList();
            return Ok(optimizedCampaigns);
        }

        [HttpGet("business/{businessId}")]
        public async Task<ActionResult<List<Campaign>>> GetByBusiness(int businessId)
        {
            var campaigns = await _campaignService.GetCampaignsByBusiness(businessId);
            var optimizedCampaigns = campaigns.Select(c => new
            {
                c.CampaignId,
                c.Title,
                c.Description,
                c.DiscountRate,
                c.StartDate,
                c.EndDate,
                c.BusinessId,
                c.CategoryId,
                Business = c.Business != null ? new
                {
                    c.Business.BusinessId,
                    c.Business.Name,
                    c.Business.Description,
                    c.Business.City
                } : null,
                Category = c.Category != null ? new
                {
                    c.Category.CategoryId,
                    c.Category.Name,
                    c.Category.Description
                } : null,
                c.IsDeleted,
                c.CreatedDate
            }).ToList();
            return Ok(optimizedCampaigns);
        }

        [HttpPost]
        public async Task<ActionResult<Campaign>> Create([FromBody] Campaign campaign)
        {
            var createdCampaign = await _campaignService.AddCampaign(campaign);
            return CreatedAtAction(nameof(GetById), new { id = createdCampaign.CampaignId }, createdCampaign);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Campaign campaign)
        {
            if (id != campaign.CampaignId)
            {
                return BadRequest();
            }
            var updatedCampaign = await _campaignService.UpdateCampaign(campaign);
            if (updatedCampaign == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _campaignService.DeleteCampaign(id);
            return NoContent();
        }
    }
} 
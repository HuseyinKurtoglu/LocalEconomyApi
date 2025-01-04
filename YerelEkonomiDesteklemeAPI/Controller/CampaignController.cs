using LocalEconomyApi.Abstract;
using LocalEconomyApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocalEconomyApi.Controllers
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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_campaignService.GetAllCampaigns());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var campaign = _campaignService.GetCampaignById(id);
                if (campaign == null)
                    return NotFound(new { message = $"Campaign with ID {id} not found." });

                return Ok(campaign);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Campaign campaign)
        {
            try
            {
                if (campaign == null)
                    return BadRequest(new { message = "Invalid campaign data." });

                _campaignService.AddCampaign(campaign);
                return CreatedAtAction(nameof(GetById), new { id = campaign.CampaignId }, campaign);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Campaign campaign)
        {
            try
            {
                if (campaign == null || campaign.CampaignId != id)
                    return BadRequest(new { message = "Campaign ID mismatch or invalid data." });

                _campaignService.UpdateCampaign(campaign);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _campaignService.DeleteCampaign(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("active")]
        public IActionResult GetActiveCampaigns()
        {
            try
            {
                return Ok(_campaignService.GetActiveCampaigns());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            try
            {
                return Ok(_campaignService.GetCampaignsByCategory(categoryId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("business/{businessId}")]
        public IActionResult GetByBusiness(int businessId)
        {
            try
            {
                return Ok(_campaignService.GetCampaignsByBusiness(businessId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

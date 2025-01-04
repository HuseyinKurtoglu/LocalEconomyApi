using LocalEconomyApi.Abstract.business;
using LocalEconomyApi.Models.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LocalEconomyApi.Controllers.business
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly DbContext _context;

        public BusinessController(IBusinessService businessService, DbContext context)
        {
            _businessService = businessService;
            _context = context;
        }

        // GET: api/Business
        [HttpGet]
        public IActionResult GetAllBusinesses()
        {
            try
            {
                var businesses = _businessService.GetAllBusinesses();
                return Ok(businesses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Business/{id}
        [HttpGet("{id}")]
        public IActionResult GetBusinessById(int id)
        {
            try
            {
                var business = _businessService.GetBusinessById(id);
                if (business == null)
                    return NotFound(new { message = $"Business with ID {id} not found." });

                return Ok(business);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST: api/Business
        [HttpPost]
        public IActionResult AddBusiness([FromBody] Business business)
        {
            try
            {
                if (business == null)
                    return BadRequest(new { message = "Invalid business data." });

                _businessService.AddBusiness(business);
                return CreatedAtAction(nameof(GetBusinessById), new { id = business.BusinessId }, business);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/Business/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBusiness(int id, [FromBody] Business business)
        {
            try
            {
                if (business == null || business.BusinessId != id)
                    return BadRequest(new { message = "Business ID mismatch or invalid data." });

                _businessService.UpdateBusiness(business);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/Business/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBusiness(int id)
        {
            try
            {
                _businessService.DeleteBusiness(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Business/City/{city}
        [HttpGet("City/{city}")]
        public IActionResult GetBusinessesByCity(string city)
        {
            try
            {
                var businesses = _businessService.GetBusinessesByCity(city);
                return Ok(businesses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("SoftDelete/{id}")]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                _businessService.SoftDeleteBusiness(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}

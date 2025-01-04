using LocalEconomyApi.Abstract;
using LocalEconomyApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocalEconomyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetFavoritesByUserId(int userId)
        {
            try
            {
                var favorites = _favoriteService.GetFavoritesByUserId(userId);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddFavorite([FromBody] Favorite favorite)
        {
            try
            {
                if (favorite == null) return BadRequest("Favori bilgisi geçersiz.");

                _favoriteService.AddFavorite(favorite);
                return CreatedAtAction(nameof(GetFavoritesByUserId), new { userId = favorite.UserId }, favorite);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{favoriteId}")]
        public IActionResult RemoveFavorite(int favoriteId)
        {
            try
            {
                _favoriteService.RemoveFavorite(favoriteId);
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

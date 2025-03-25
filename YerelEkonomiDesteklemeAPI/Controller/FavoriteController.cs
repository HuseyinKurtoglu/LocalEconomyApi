using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;
using YerelEkonomiDestekleme.Business.Abstract;

namespace YerelEkonomiDesteklemeAPI.Controllers
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

        [HttpGet]
        public async Task<ActionResult<List<Favorite>>> GetAllFavorites()
        {
            var favorites = await _favoriteService.GetAllFavorites();
            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavoriteById(int id)
        {
            var favorite = await _favoriteService.GetFavoriteById(id);
            if (favorite == null)
            {
                return NotFound();
            }
            return Ok(favorite);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Favorite>>> GetFavoritesByUser(string userId)
        {
            var favorites = await _favoriteService.GetFavoritesByUser(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<ActionResult<Favorite>> CreateFavorite(Favorite favorite)
        {
            var createdFavorite = await _favoriteService.AddFavorite(favorite);
            return CreatedAtAction(nameof(GetFavoriteById), new { id = createdFavorite.FavoriteId }, createdFavorite);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var favorite = await _favoriteService.GetFavoriteById(id);
            if (favorite == null)
            {
                return NotFound();
            }

            await _favoriteService.DeleteFavorite(id);
            return NoContent();
        }
    }
}

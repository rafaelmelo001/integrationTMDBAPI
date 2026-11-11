using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMDBAPI.Data;
using TMDBAPI.Models;

namespace TMDBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddFavorite(FavoriteMovie favorite)
        {
           var alreadyExist = await _context.FavoriteMovies
           .AnyAsync(f => f.MovieId == favorite.MovieId);

           if(alreadyExist)
            {
                return Conflict("Filme ja existe nos favoritos");
            }

            _context.FavoriteMovies.Add(favorite);

            await _context.SaveChangesAsync();

            return Ok(favorite);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorite()
        {

            var dados = await _context.FavoriteMovies.ToListAsync();
            
            return Ok(dados);
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateFavorite(FavoriteMovie favorite, int id)
        {
            var filme = await _context.FavoriteMovies.FindAsync(id);

            if(filme == null)
            {
                return NotFound("Nenhum filme encontrado");
            }

            filme.MovieId = favorite.MovieId;
            filme.Title = favorite.Title;
            filme.PosterPath = favorite.PosterPath;

            await _context.SaveChangesAsync();

            return Ok(filme);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var filme = await _context.FavoriteMovies.FindAsync(id);

            if(filme == null)
            {
                return NotFound("Nenhum filme encontrado");
            }

            _context.FavoriteMovies.Remove(filme);

            await _context.SaveChangesAsync();

            return Ok();


        }
    }
}
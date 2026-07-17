using Microsoft.AspNetCore.Mvc;
using TMDBAPI.Services;


namespace TMDBAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilmAsync([FromQuery] string moviename)
        {
            var response = await _movieService.GetFilmAsync(moviename);

            return Ok(response);
        }

        
    }
    
}
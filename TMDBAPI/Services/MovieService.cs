using System.Net.Http.Headers;

namespace TMDBAPI.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        private readonly string _token ="";

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFilmAsync(string moviename)
        {
            var urlMovie = $"https://api.themoviedb.org/3/search/movie?query={moviename}";

            _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _token);

            var response = await _httpClient.GetAsync(urlMovie);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json;
            
        }
    }
}
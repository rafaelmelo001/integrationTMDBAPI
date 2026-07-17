using System.Net.Http.Headers;

namespace TMDBAPI.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        private readonly string _token ="eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5MWQyZDRhMmUzMzU2ZTRhZDczYzBhMmU2ODlhM2FlZCIsIm5iZiI6MTc4NDMwNzc3Ni45MDMsInN1YiI6IjZhNWE2MDQwY2U4YzA4ZTRkOTcwODExNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.24OUpckqIPRtYv-MzRiPca98D2xYGCMy4Z3KFR6jHAg";

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
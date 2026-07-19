using System.Net.Http.Headers;
using TMDBAPI.Models;
using TMDBAPI.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;

namespace TMDBAPI.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;

        private readonly string _token;
        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _token = Environment.GetEnvironmentVariable("TMDBAPI")
                ?? throw new Exception("TMDBKEY não configurada");
        }

        public async Task<MovieResponseDTO> GetFilmAsync(string moviename)
        {
            var urlMovie = $"https://api.themoviedb.org/3/search/movie?query={moviename}";

            _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _token);

            var response = await _httpClient.GetAsync(urlMovie);

            response.EnsureSuccessStatusCode();
            
            ///desserialização
            var movieResponseModel = await response.Content.ReadFromJsonAsync<MovieResponseModel>()///desserialização
                ?? throw new InvalidOperationException("Não foi possível desserializar a resposta da TMDB");
            //coverter list<movieModel> para list<MovieDTO>
            //Mapeamento
            List<MovieDTO> movieDtos = [];//neste momento movieDTOs é uma lista vazia
            foreach(var movie in movieResponseModel.results)//percorrendo a propriedade "results" que é uma lista<MovieMOdel>
            {                                      //a cada volta a variavel movie recebe um único objeto MovieModel.
                MovieDTO dto = new MovieDTO();//a cada iteração, cada filme ganha seu proprio objeto MovieDTO, cada um com sua informação.
                dto.Title = movie.title;//lado esq DTO e dir MODEL, preenchendo a priopriedade Title do DTO. Copia o valor da Model para o DTO.
                dto.Overview = movie.overview;
                dto.PosterPath = movie.poster_path;
                dto.ReleaseDate = movie.release_date;
                dto.VoteAverage = movie.vote_average;

                //preencher
                movieDtos.Add(dto);
            }

            MovieResponseDTO responseDTO = new MovieResponseDTO()//criamos o objeto
            {
                Page = movieResponseModel.page,
                TotalPages = movieResponseModel.total_pages,
                TotalResults = movieResponseModel.total_results,
                Results = movieDtos

                
            };
            return responseDTO;


        }
    }
}

// Desserialização: transforma JSON em Models.
// Mapeamento: transforma Models em DTOs.
// Serialização: transforma os DTOs novamente em JSON para enviar ao cliente.
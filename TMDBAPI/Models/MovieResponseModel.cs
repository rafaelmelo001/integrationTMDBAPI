namespace TMDBAPI.Models
{
    public class MovieResponseModel
    {
        public int page {get; set;}
        public List<MovieModel> results {get; set;} =[];//composição de objetos com list<MovieModel>
                                                        //a response contém varios filmes com title, overview etc,
                                                        //que esta no MovieModel, cada MovieModel reepsenta 1 filmes
        public int total_pages {get; set;}
        public int total_results {get; set;}
    }
}
///MovieResponseModel representa a resposta da TMDB, 
//por isso contém a MovieModel.
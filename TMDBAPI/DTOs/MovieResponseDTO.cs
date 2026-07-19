namespace TMDBAPI.DTOs
{
    public class MovieResponseDTO
    {
        public int Page { get; set; }

        public List<MovieDTO> Results { get; set; } = [];

        public int TotalPages { get; set; }

        public int TotalResults { get; set; }
    }
}

///MovieResponseDTO conhece a MovieDTO - MovieResponseDTO representa a responsta da minha API,
//por isso contém MovieDTO.
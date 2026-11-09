
export async function getFilm(moviename)
{

    const response = await fetch(`https://integrationtmdbapi.onrender.com/api/Movie?moviename=${moviename}`);
    
    if(!response.ok)
    {
        throw new Error("Algum problema ao comunicar com API")
    }

    const dados = await response.json();

    return dados;
}

export async function addFavorite(filme) {

    const response = await fetch("https://integrationtmdbapi.onrender.com/api/Favorites", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            movieId: filme.id,
            title: filme.title,
            posterPath: filme.posterPath
        })
    });

    if (!response.ok) {
        throw new Error("Erro ao adicionar favorito");
    }

    return await response.json();
}
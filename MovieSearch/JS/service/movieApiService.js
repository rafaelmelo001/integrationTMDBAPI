
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

export async function getFavorites() {

    const response = await fetch(
        "https://integrationtmdbapi.onrender.com/api/Favorites"
    );

    if (!response.ok) {
        throw new Error("Erro ao buscar favoritos");
    }

    return await response.json();
}

export async function deleteFavorite(id) {

    const response = await fetch(
        `https://integrationtmdbapi.onrender.com/api/Favorites/${id}`,
        {
            method: "DELETE"
        }
    );

    if (!response.ok) {
        throw new Error("Erro ao excluir favorito");
    }
}
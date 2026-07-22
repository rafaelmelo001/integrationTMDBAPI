
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
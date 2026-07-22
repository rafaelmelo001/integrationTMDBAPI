
export async function getFilm(moviename)
{

    const response = await fetch(`http://localhost:3000/api/Movie?moviename=${moviename}`);
    
    if(!response.ok)
    {
        throw new Error("Algum problema ao comunicar com API")
    }

    const dados = await response.json();

    return dados;
}
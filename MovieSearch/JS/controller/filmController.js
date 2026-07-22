import {getFilm} from "../service/movieApiService.js"


const display = document.getElementById("showContent");
const message = document.getElementById("message");
const loading = document.getElementById("loading");
const button = document.getElementById("getFilm");

function showLoading() {
    loading.classList.add("show");
    button.disabled = true;
}

function hideLoading() {
    loading.classList.remove("show");
    button.disabled = false;
}

document.getElementById("getFilm").addEventListener("click", async function (){

    try {

        const moviename = document.getElementById("filme").value.trim();

        if (moviename === "") {

            message.innerText = "Informe um filme que queira buscar.";
            display.classList.remove("show");

            return;
        }

        message.innerText = "";

        display.classList.remove("show");
        showLoading();

        const dados = await getFilm(moviename);
        
        display.innerHTML = "";
        dados.results.forEach(filme => {
            console.log(filme)
            display.innerHTML += `
            
            <div class="movie-card">
            <img src="https://image.tmdb.org/t/p/w500${filme.posterPath}" alt="${filme.title}">

            <div class="movie-info">
                <h2>${filme.title}</h2>

                <p><strong>Sinopse:</strong> ${filme.overview}</p>
                <p><strong>Lançamento:</strong> ${filme.releaseDate}</p>
                <p><strong>Nota:</strong> ${filme.voteAverage}</p>
            </div>
        </div>
        `; 
        });
       

        display.classList.add("show");

    }
    catch (error) {

        display.classList.remove("show");
        message.innerText = "Erro ao buscar o filme.";

        console.error(error);

    }
    finally {

        hideLoading();

    }
});
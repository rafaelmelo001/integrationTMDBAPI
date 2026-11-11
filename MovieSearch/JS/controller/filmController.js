import {getFilm, addFavorite, getFavorites, deleteFavorite} from "../service/movieApiService.js"


const display = document.getElementById("showContent");
const message = document.getElementById("message");
const loading = document.getElementById("loading");
const button = document.getElementById("getFilm");
/*Favorite-Button*/
const favoritesTab = document.getElementById("favoritesTab");
const favoritesContent = document.getElementById("favoritesContent");
const searchBox = document.querySelector(".search-box");
const favoritesList = document.getElementById("favoritesList");
/*Favorite-Button*/





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
        
            display.innerHTML += `
            
            <div class="movie-card">
            <img src="https://image.tmdb.org/t/p/w500${filme.posterPath}" alt="${filme.title}">

            <div class="movie-info">
                <h2>${filme.title}</h2>

                <p><strong>Sinopse:</strong> ${filme.overview}</p>
                <p><strong>Lançamento:</strong> ${filme.releaseDate}</p>
                <p><strong>Nota:</strong> ${filme.voteAverage}</p>
            </div>
            <button
                class="favorite-button"
                data-movie-id="${filme.id}"
                data-title="${filme.title}"
                data-poster="${filme.posterPath}">
                Adicionar aos favoritos
            </button>
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

display.addEventListener("click", async function (event) {

    if (event.target.classList.contains("favorite-button")) {

        const movieId = Number(event.target.dataset.movieId);

        const filme = {
        id: Number(event.target.dataset.movieId),
        title: event.target.dataset.title,
        posterPath: event.target.dataset.poster
    };

    try {
        
        await addFavorite(filme);

        alert("Filme adicionado aos favoritos!");
    }
    catch (error) {
        console.error(error);
        alert("Erro ao adicionar favorito.");
    }
    }

});

favoritesTab.addEventListener("click", async function () {

    const showingFavorites = favoritesContent.classList.contains("show");

    if (showingFavorites) {

        favoritesContent.classList.remove("show");
        searchBox.classList.remove("hidden");

        favoritesTab.innerText = "Meus favoritos";

    } else {

        favoritesContent.classList.add("show");
        searchBox.classList.add("hidden");
        display.classList.remove("show");

        favoritesTab.innerText = "Voltar à pesquisa";

        try {

            const favoritos = await getFavorites();

            favoritesList.innerHTML = "";

            favoritos.forEach(filme => {

                favoritesList.innerHTML += `
                    <div class="movie-card">

                        <img 
                            src="https://image.tmdb.org/t/p/w500${filme.posterPath}"
                            alt="${filme.title}"
                        >
                        
                        <div class="movie-info">
                            <h2>${filme.title}</h2>

                            <button 
                                class="delete-favorite"
                                data-id="${filme.id}">
                                Excluir dos favoritos
                            </button>
                        </div>

                    </div>
                `;

            });

        } catch (error) {

            console.error(error);
            favoritesList.innerHTML = "<p>Erro ao carregar favoritos.</p>";

        }
    }

});

favoritesList.addEventListener("click", async function (event) {

    if (event.target.classList.contains("delete-favorite")) {

        const id = Number(event.target.dataset.id);

        try {

            await deleteFavorite(id);

            alert("Filme removido dos favoritos!");

            // Atualiza a lista após a exclusão
            const favoritos = await getFavorites();

            favoritesList.innerHTML = "";

            favoritos.forEach(filme => {

                favoritesList.innerHTML += `
                    <div class="movie-card">
                        <img 
                            src="https://image.tmdb.org/t/p/w500${filme.posterPath}"
                            alt="${filme.title}"
                        >

                        <div class="movie-info">
                            <h2>${filme.title}</h2>

                            <button 
                                class="delete-favorite"
                                data-id="${filme.id}">
                                Excluir dos favoritos
                            </button>
                        </div>
                    </div>
                `;
            });

        } catch (error) {

            console.error(error);
            alert("Erro ao excluir favorito.");

        }
    }
});
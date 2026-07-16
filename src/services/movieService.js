import Vue from "vue";
import { API_BASE_URL } from "./api";

const MOVIE_URL = `${API_BASE_URL}/movies`;
const MULTIPART_HEADERS = {
    headers: {
        "Content-Type": "multipart/form-data"
    }
};

function createMovieFormData(movie) {
    const formData = new FormData();

    formData.append("Name", movie.name);
    formData.append("YearOfRelease", movie.yearOfRelease);
    formData.append("Plot", movie.plot);
    formData.append("ProducerId", movie.producerId);

    movie.actorIds.forEach((id) => formData.append("actorIds", id));
    movie.genreIds.forEach((id) => formData.append("genreIds", id));

    if (movie.coverImage) {
        formData.append("CoverImage", movie.coverImage);
    }

    return formData;
}

export default {

    getMovies() {
        return Vue.http.get(MOVIE_URL);
    },

    getMovieById(id) {
        return Vue.http.get(`${MOVIE_URL}/${id}`);
    },

    createMovie(movie) {
        return Vue.http.post(
            MOVIE_URL,
            createMovieFormData(movie),
            MULTIPART_HEADERS
        );
    },

    updateMovie(id, movie) {
        return Vue.http.put(
            `${MOVIE_URL}/${id}`,
            createMovieFormData(movie),
            MULTIPART_HEADERS
        );
    },

    deleteMovie(id) {
        return Vue.http.delete(`${MOVIE_URL}/${id}`);
    }

};

import Vue from "vue";
import { API_BASE_URL } from "./api";

const MOVIE_URL = `${API_BASE_URL}/movies`;

export default {

    getMovies() {
        return Vue.http.get(MOVIE_URL);
    },

    getMovieById(id) {
        return Vue.http.get(`${MOVIE_URL}/${id}`);
    },

    createMovie(formData) {
        return Vue.http.post(
            MOVIE_URL,
            formData,
            {
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            }
        );
    },

    updateMovie(id, formData) {
        return Vue.http.put(
            `${MOVIE_URL}/${id}`,
            formData,
            {
                headers: {
                    "Content-Type": "multipart/form-data"
                }
            }
        );
    },

    deleteMovie(id) {
        return Vue.http.delete(`${MOVIE_URL}/${id}`);
    }

};

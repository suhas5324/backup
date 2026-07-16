import Vue from "vue";
import { API_BASE_URL } from "./api";

const GENRE_URL = `${API_BASE_URL}/genres`;

export default {

    getGenres() {
        return Vue.http.get(GENRE_URL);
    }

};

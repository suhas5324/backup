import Vue from "vue";
import { API_BASE_URL } from "./api";

const ACTOR_URL = `${API_BASE_URL}/actors`;

export default {

    getActors() {
        return Vue.http.get(ACTOR_URL);
    },

    createActor(actor) {
        return Vue.http.post(ACTOR_URL, actor);
    }

};

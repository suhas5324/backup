import Vue from "vue";
import { API_BASE_URL } from "./api";

const PRODUCER_URL = `${API_BASE_URL}/producers`;

export default {

    getProducers() {
        return Vue.http.get(PRODUCER_URL);
    },

    createProducer(producer) {
        return Vue.http.post(PRODUCER_URL, producer);
    }

};

import Vue from "vue";
import Router from "vue-router";

import MovieListView from "@/views/MovieListView.vue";
import MovieEditorView from "@/views/MovieEditorView.vue";

Vue.use(Router);

export default new Router({

    mode: "history",

    routes: [

        {
            path: "/",
            name: "movies",
            component: MovieListView
        },

        {
            path: "/create",
            name: "createMovie",
            component: MovieEditorView
        },

        {
            path: "/edit/:id",
            name: "editMovie",
            component: MovieEditorView
        }

    ]

});

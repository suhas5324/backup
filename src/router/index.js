import Vue from "vue";
import Router from "vue-router";

import MovieListView from "@/views/MovieListView.vue";
import CreateMovieView from "@/views/CreateMovieView.vue";
import EditMovieView from "@/views/EditMovieView.vue";

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
            component: CreateMovieView
        },

        {
            path: "/edit/:id",
            name: "editMovie",
            component: EditMovieView,
            props: true
        }

    ]

});
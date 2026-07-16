<template>
  <v-container>

    <MovieForm
      :actors="allActors"
      :producers="allProducers"
      :genres="allGenres"

      @movie-submit="createMovie"
      @person-save="createPerson"
    />

  </v-container>
</template>

<script>
import MovieForm from "@/components/MovieForm.vue";
import movieEditor from "@/mixins/movieEditor";
import createMovieFormData from "@/utils/movieFormData";
import { mapActions } from "vuex";

export default {

  name: "CreateMovieView",

  components: {
    MovieForm
  },

  mixins: [movieEditor],

  async created() {
    await this.loadLookupData();
  },

  methods: {

    ...mapActions(["createMovieAction"]),

  async createMovie(movie) {
    try {
      await this.createMovieAction(createMovieFormData(movie));
      this.$router.push({ name: "movies" });
    } catch (error) {
      console.error(error);
    }
  }

  }

}
</script>


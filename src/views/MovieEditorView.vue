<template>
  <v-container>
    <MovieForm
      v-if="!isEdit || movie"
      :movie="movie"
      @movie-submit="saveMovie"
    />
  </v-container>
</template>

<script>
import MovieForm from "@/components/MovieForm.vue";
import { mapActions } from "vuex";

export default {
  name: "MovieEditorView",

  components: {
    MovieForm,
  },

  data() {
    return {
      movie: null,
    };
  },

  computed: {
    movieId() {
      return this.$route.params.id;
    },

    isEdit() {
      return !!this.movieId;
    },
  },

  async created() {
    if (this.isEdit) {
      await this.loadMovie();
    }
  },

  methods: {
    ...mapActions([
      "createMovieAction",
      "fetchMovieByIdAction",
      "updateMovieAction",
    ]),

    async loadMovie() {
      try {
        this.movie = await this.fetchMovieByIdAction(this.movieId);
      } catch (error) {
        console.error(error);
      }
    },

    async saveMovie(movie) {
      try {
        if (this.isEdit) {
          await this.updateMovieAction({
            id: this.movieId,
            movie,
          });
        } else {
          await this.createMovieAction(movie);
        }

        this.$router.push({ name: "movies" });
      } catch (error) {
        console.error(error);
      }
    },
  },
};
</script>

<template>
  <v-container>
    <v-row>
      <v-col
        cols="12"
        sm="3"
        class="d-flex justify-center"
        v-for="movie in allMovies"
        :key="movie.id"
      >
        <MovieCard
          :movie="movie"
          @explore="openMovie"
          @edit="editMovie"
          @delete="openDeleteDialog"
        />
      </v-col>
    </v-row>

    <!-- Movie Details -->

    <MovieDetailsDialog
      :dialog="movieDialog"
      :movie="selectedMovie"
      @close="movieDialog = false"
    />

    <!-- Delete Dialog -->

    <DeleteConfirmationDialog
      :dialog="deleteDialog"
      title="Delete Movie"
      :message="deleteMessage"
      @close="closeDeleteDialog"
      @confirm="deleteMovie"
    />
  </v-container>
</template>

<script>
import MovieCard from "@/components/MovieCard.vue";
import MovieDetailsDialog from "@/components/MovieDetailsDialog.vue";
import DeleteConfirmationDialog from "@/components/DeleteMovieDialog.vue";

import { mapGetters, mapActions } from "vuex";

export default {
  name: "MovieListView",

  components: {
    MovieCard,
    MovieDetailsDialog,
    DeleteConfirmationDialog,
  },

  data() {
    return {
      movieDialog: false,
      deleteDialog: false,
      selectedMovie: null,
    };
  },

  computed: {
    ...mapGetters({
      allMovies: "allMovies",
    }),

    deleteMessage() {
      if (!this.selectedMovie) {
        return "";
      }
      return `Are you sure you want to delete "${this.selectedMovie.name}"?`;
    },
  },

  async created() {
    await this.fetchMoviesAction();
  },

  methods: {
    ...mapActions(["fetchMoviesAction", "deleteMovieAction"]),

    openMovie(movie) {
      this.selectedMovie = movie;
      this.movieDialog = true;
    },

    editMovie(movie) {
      this.$router.push({
        name: "editMovie",
        params: { id: movie.id },
      });
    },

    openDeleteDialog(movie) {
      this.selectedMovie = movie;
      this.deleteDialog = true;
    },

    closeDeleteDialog() {
      this.deleteDialog = false;
      this.selectedMovie = null;
    },

    async deleteMovie() {
      try {
        await this.deleteMovieAction(this.selectedMovie.id);
        this.closeDeleteDialog();
      } catch (error) {
        console.error(error);
      }
    },
  },
};
</script>
<template>
  <v-container>

    <MovieForm
      v-if="movie"

      :movie="movie"

      :isEdit="true"

      :actors="allActors"

      :producers="allProducers"

      :genres="allGenres"

      @movie-submit="updateMovie"

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

  name: "EditMovieView",

  components: {
    MovieForm
  },

  mixins: [movieEditor],

  data() {

    return {

      movie: null

    }

  },

  async created() {

    await this.loadData();

  },

  methods: {

    ...mapActions(["fetchMovieByIdAction", "updateMovieAction"]),

    async loadData() {

      try {

        await this.loadLookupData();

        this.movie = await this.fetchMovieByIdAction(
          this.$route.params.id
        );

      }

      catch (error) {

        console.error(error);

      }

    },

    async updateMovie(movie) {

      try {

        await this.updateMovieAction({
          id: this.$route.params.id,
          formData: createMovieFormData(movie)
        });

        this.$router.push({

          name: "movies"

        });

      }

      catch (error) {

        console.error(error);

      }

    }

  }

}
</script>


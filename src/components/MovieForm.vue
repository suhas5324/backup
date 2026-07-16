<template>
  <v-container>
    <v-card elevation="4">
      <v-card-title>
        {{ isEdit ? "Edit Movie" : "Create Movie" }}
      </v-card-title>

      <v-divider></v-divider>

      <v-card-text>
        <v-form ref="movieForm" v-model="valid">
          <!-- Movie Name -->

          <v-text-field
            label="Movie Name"
            v-model="movieData.name"
            :rules="movieNameRules"
            outlined
            dense
            required
          ></v-text-field>

          <!-- Year Of Release -->

          <v-text-field
            label="Year Of Release"
            type="number"
            v-model="movieData.yearOfRelease"
            :rules="yearRules"
            outlined
            dense
            required
          ></v-text-field>

          <!-- Plot -->

          <v-textarea
            label="Plot"
            rows="4"
            v-model="movieData.plot"
            :rules="plotRules"
            outlined
            required
          ></v-textarea>

          <!-- Producer -->

          <v-row align="center">
            <v-col cols="9">
              <v-select
                label="Producer"
                :items="producers"
                item-text="name"
                item-value="id"
                v-model="movieData.producerId"
                :rules="producerRules"
                outlined
                dense
              ></v-select>
            </v-col>

            <v-col cols="3" class="d-flex align-center">
              <v-btn color="primary" block @click="openProducerDialog">
                Add Producer
              </v-btn>
            </v-col>
          </v-row>

          <!-- Actors -->

          <v-row align="center">
            <v-col cols="9">
              <v-select
                label="Actors"
                :items="actors"
                item-text="name"
                item-value="id"
                multiple
                chips
                v-model="movieData.actorIds"
                :rules="actorRules"
                :menu-props="{ closeOnContentClick: true }"
                outlined
                dense
              ></v-select>
            </v-col>

            <v-col cols="3" class="d-flex align-center">
              <v-btn color="primary" block @click="openActorDialog">
                Add Actor
              </v-btn>
            </v-col>
          </v-row>

          <!-- Genres -->

          <v-select
            label="Genres"
            :items="genres"
            item-text="name"
            item-value="id"
            multiple
            chips
            v-model="movieData.genreIds"
            :rules="genreRules"
            :menu-props="{ closeOnContentClick: true }"
            outlined
            dense
          ></v-select>

          <!-- Poster -->

          <v-file-input
            label="Poster Image"
            accept="image/*"
            v-model="movieData.coverImage"
            :rules="posterRules"
            outlined
            dense
          ></v-file-input>

          <v-divider class="my-4"></v-divider>

          <div class="text-right">
            <v-btn color="primary" @click="submitMovie">
              {{ isEdit ? "Update Movie" : "Create Movie" }}
            </v-btn>
          </div>
        </v-form>
      </v-card-text>
    </v-card>

    <!-- Person Dialog -->

    <PersonDialog
      :dialog="personDialog"
      :entityType="dialogType"
      @close="personDialog = false"
      @person-save="savePerson"
    />
  </v-container>
</template>

<script>
import PersonDialog from "@/components/PersonDialog.vue";
import { mapActions, mapGetters } from "vuex";

export default {
  name: "MovieForm",

  components: {
    PersonDialog,
  },

  props: {
    movie: {
      type: Object,

      default: null,
    },
  },

  data() {
    return {
      valid: false,

      personDialog: false,

      dialogType: "",

      movieData: {
        name: "",

        yearOfRelease: "",

        plot: "",

        producerId: null,

        actorIds: [],

        genreIds: [],

        coverImage: null,
      },

      movieNameRules: [(v) => !!v || "Movie name is required"],

      yearRules: [
        (v) => !!v || "Year is required",

        (v) => Number(v) > 1800 || "Enter valid year",
      ],

      plotRules: [(v) => !!v || "Plot is required"],

      producerRules: [(v) => !!v || "Producer is required"],

      actorRules: [(v) => v.length > 0 || "Select at least one actor"],

      genreRules: [(v) => v.length > 0 || "Select at least one genre"],

      posterRules:  [(v) => !!v || "Poster image is required"],
    };
  },
  computed: {
    ...mapGetters({
      actors: "allActors",
      producers: "allProducers",
      genres: "allGenres",
    }),

    isEdit() {
      return !!this.movie;
    },
  },

  async created() {
    await this.loadLookupData();
  },

  watch: {
    movie: {
      immediate: true,

      handler(newMovie) {
        if (!newMovie) {
          return;
        }

        this.movieData = {
          name: newMovie.name,

          yearOfRelease: newMovie.yearOfRelease,

          plot: newMovie.plot,

          producerId: newMovie.producerId,

          actorIds: newMovie.actorIds || [],

          genreIds: newMovie.genreIds || [],

          coverImage: null,
        };
      },
    },
  },
  methods: {
    ...mapActions([
      "createActorAction",
      "createProducerAction",
      "fetchActorsAction",
      "fetchGenresAction",
      "fetchProducersAction",
    ]),

    loadLookupData() {
      return Promise.all([
        this.fetchActorsAction(),
        this.fetchProducersAction(),
        this.fetchGenresAction(),
      ]);
    },

    openActorDialog() {
      this.dialogType = "actor";

      this.personDialog = true;
    },

    openProducerDialog() {
      this.dialogType = "producer";

      this.personDialog = true;
    },

    async savePerson({ entityType, person }) {
      if (entityType === "actor") {
        await this.createActorAction(person);
      } else {
        await this.createProducerAction(person);
      }

      this.personDialog = false;
    },

    submitMovie() {
      if (!this.$refs.movieForm.validate()) {
        return;
      }

      const payload = {
        name: this.movieData.name,

        yearOfRelease: Number(this.movieData.yearOfRelease),

        plot: this.movieData.plot,

        producerId: this.movieData.producerId,

        actorIds: [...this.movieData.actorIds],

        genreIds: [...this.movieData.genreIds],

        coverImage: this.movieData.coverImage,
      };

      this.$emit("movie-submit", payload);
    },
  },
};
</script>

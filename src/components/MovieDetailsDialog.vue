<template>
  <v-dialog
    :value="dialog"
    max-width="700"
    @input="closeDialog"
  >
    <v-card>

      <v-card-title class="headline">
        Movie Details
      </v-card-title>

      <v-divider></v-divider>

      <v-card-text v-if="movie">

        <v-row>

          <v-col cols="12" md="4">

            <v-img
              :src="movie.coverImage"
              height="300"
              contain
            ></v-img>

          </v-col>

          <v-col cols="12" md="8">

            <p>
              <strong>Name:</strong>
              {{ movie.name }}
            </p>

            <p>
              <strong>Year of Release:</strong>
              {{ movie.yearOfRelease }}
            </p>

            <p>
              <strong>Plot:</strong>
              {{ movie.plot }}
            </p>

          </v-col>

        </v-row>

      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>

        <v-spacer></v-spacer>

        <v-btn
          color="primary"
          text
          @click="closeDialog"
        >
          Close
        </v-btn>

      </v-card-actions>

    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "MovieDetailsDialog",

  props: {
    dialog: {
      type: Boolean,
      required: true
    },

    movie: {
      type: Object,
      default: null
    }
  },

  methods: {
    closeDialog() {
      this.$emit("close");
    }
  }
};
</script>

<style scoped>
p {
  margin-bottom: 12px;
}
</style>
<template>
  <v-app-bar app color="primary" dark>

    <v-app-bar-title class="font-weight-bold">
      MovieHUB
    </v-app-bar-title>

    <v-spacer></v-spacer>
    <v-btn
      text
      :to="{ name: 'movies' }"
    >
      Movies
    </v-btn>

    <v-btn
      color="white"
      class="warning--text ml-2"
      :to="{ name: 'createMovie' }"
    >
      Create Movie
    </v-btn>
  </v-app-bar>
</template>

<script>
export default {
  name: "AppBar"
};
</script>


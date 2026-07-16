<template>
  <v-card class="mx-auto movie-card" elevation="3">
    <!-- Movie Poster -->
    <div class="movie-poster">
      <v-img :src="movie.coverImage" cover></v-img>
    </div>

    <!-- Movie Details -->
    <v-card-title class="font-weight-bold movie-title">
      {{ movie.name }}
    </v-card-title>

    <v-card-text>
      {{ movie.plot }}
    </v-card-text>

    <!-- Buttons -->
    <v-card-actions class="mt-auto movie-actions">
      <v-btn
        color="primary"
        small
        class="movie-action-btn"
        @click="$emit('explore', movie)"
      >
        Explore
      </v-btn>

      <v-btn
        color="warning"
        small
        class="movie-action-btn"
        @click="$emit('edit', movie)"
        >Edit</v-btn
      >

      <v-btn
        color="error"
        small
        class="movie-action-btn"
        @click="$emit('delete', movie)"
        >Delete</v-btn
      >
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "MovieCard",

  props: {
    movie: {
      type: Object,
      required: true,
    },
  },
};
</script>

<style scoped>
.movie-card {
  height: 100%;
  display: flex;
  flex-direction: column;
  width: 100%;
  max-width: 320px;
  margin-left: auto;
  margin-right: auto;
  border-radius: 10px;
  overflow: hidden;
  /* transition: transform 0.2s ease, box-shadow 0.2s ease; */
}

.movie-poster {
  height: 240px;
  overflow: hidden;
  background: #f5f5f5;
}

.movie-poster .v-image {
  height: 100%;
}

.movie-card .movie-title {
  min-height: 64px;
  display: flex;
  align-items: center;
  word-break: break-word;
}

.movie-card .v-card__text {
  flex-grow: 1;
}

.movie-card .v-card__actions {
  margin-top: auto;
  flex-wrap: wrap;
  gap: 6px;
  padding: 8px 12px 14px;
  justify-content: space-between;
  align-items: center;
}

.movie-card .v-card__actions .movie-action-btn {
  margin: 0;
  flex: 1 1 auto;
  min-width: 0;
  font-size: 0.78rem;
  padding: 0 6px;
}

/* Mobile: stack card content a bit tighter, keep buttons inside card */
@media (max-width: 960px) {
  .movie-card {
    max-width: 100%;
  }

  .movie-poster {
    height: 200px;
  }

  .movie-card .v-card__actions .movie-action-btn {
    font-size: 0.75rem;
    padding: 0 4px;
  }
}
</style>
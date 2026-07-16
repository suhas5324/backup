export default {
  SET_MOVIES(state, movies) {
    state.movies = movies;
  },

  SET_ACTORS(state, actors) {
    state.actors = actors;
  },

  SET_PRODUCERS(state, producers) {
    state.producers = producers;
  },

  SET_GENRES(state, genres) {
    state.genres = genres;
  },

  SET_LOADING(state, loading) {
    state.loading = loading;
  },

  SET_ERROR(state, error) {
    state.error = error;
  }
};

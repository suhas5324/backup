export default {

  allMovies(state) {
    return state.movies;
  },

  allActors(state) {
    return state.actors;
  },

  allProducers(state) {
    return state.producers;
  },

  allGenres(state) {
    return state.genres;
  },

  loading(state) {
    return state.loading;
  },

  error(state) {
    return state.error;
  }
};

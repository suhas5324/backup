import movieService from "@/services/movieService";
import actorService from "@/services/actorService";
import producerService from "@/services/producerService";
import genreService from "@/services/genreService";

function clearError(commit) {
  commit("SET_ERROR", null);
}

function storeError(commit, error) {
  commit("SET_ERROR", error);
  throw error;
}

export default {
  async fetchMoviesAction({ commit }) {
    try {
      clearError(commit);
      commit("SET_LOADING", true);
      const response = await movieService.getMovies();
      commit("SET_MOVIES", response.body);
    } catch (error) {
      storeError(commit, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async fetchMovieByIdAction({ commit }, id) {
    try {
      clearError(commit);
      commit("SET_LOADING", true);
      const response = await movieService.getMovieById(id);
      return response.body;
    } catch (error) {
      storeError(commit, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async createMovieAction({ dispatch, commit }, formData) {
    try {
      clearError(commit);
      commit("SET_LOADING", true);
      await movieService.createMovie(formData);
      await dispatch("fetchMoviesAction");
    } catch (error) {
      storeError(commit, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async updateMovieAction({ dispatch, commit }, payload) {
    try {
      clearError(commit);
      commit("SET_LOADING", true);
      await movieService.updateMovie(payload.id, payload.formData);
      await dispatch("fetchMoviesAction");
    } catch (error) {
      storeError(commit, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async deleteMovieAction({ dispatch, commit }, id) {
    try {
      clearError(commit);
      commit("SET_LOADING", true);
      await movieService.deleteMovie(id);
      await dispatch("fetchMoviesAction");
    } catch (error) {
      storeError(commit, error);
    } finally {
      commit("SET_LOADING", false);
    }
  },

  async fetchActorsAction({ commit }) {
    try {
      clearError(commit);
      const response = await actorService.getActors();
      commit("SET_ACTORS", response.body);
    } catch (error) {
      storeError(commit, error);
    }
  },

  async createActorAction({ dispatch, commit }, actor) {
    try {
      clearError(commit);
      const response = await actorService.createActor(actor);
      await dispatch("fetchActorsAction");
      return response.body;
    } catch (error) {
      storeError(commit, error);
    }
  },

  async fetchProducersAction({ commit }) {
    try {
      clearError(commit);
      const response = await producerService.getProducers();
      commit("SET_PRODUCERS", response.body);
    } catch (error) {
      storeError(commit, error);
    }
  },

  async createProducerAction({ dispatch, commit }, producer) {
    try {
      clearError(commit);
      const response = await producerService.createProducer(producer);
      await dispatch("fetchProducersAction");
      return response.body;
    } catch (error) {
      storeError(commit, error);
    }
  },

  async fetchGenresAction({ commit }) {
    try {
      clearError(commit);
      const response = await genreService.getGenres();
      commit("SET_GENRES", response.body);
    } catch (error) {
      storeError(commit, error);
    }
  }
};

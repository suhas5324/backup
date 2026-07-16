import { mapActions, mapGetters } from "vuex";

export default {
  computed: {
    ...mapGetters({
      allActors: "allActors",
      allProducers: "allProducers",
      allGenres: "allGenres"
    })
  },

  methods: {
    ...mapActions({
      fetchActorsAction: "fetchActorsAction",
      fetchProducersAction: "fetchProducersAction",
      fetchGenresAction: "fetchGenresAction",
      createActorAction: "createActorAction",
      createProducerAction: "createProducerAction"
    }),

    loadLookupData() {
      return Promise.all([
        this.fetchActorsAction(),
        this.fetchProducersAction(),
        this.fetchGenresAction()
      ]);
    },

    async createPerson({ entityType, person }) {
      if (entityType === "actor") {
        await this.createActorAction(person);
        return;
      }

      await this.createProducerAction(person);
    }
  }
};

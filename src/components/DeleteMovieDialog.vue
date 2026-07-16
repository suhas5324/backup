<template>
  <v-dialog
    :value="dialog"
    max-width="450"
    @input="closeDialog"
  >
    <v-card>

      <v-card-title class="headline">
        {{ title }}
      </v-card-title>

      <v-card-text>
        {{ message }}
      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>

        <v-spacer></v-spacer>

        <v-btn
          text
          color="grey"
          @click="closeDialog"
        >
          Cancel
        </v-btn>

        <v-btn
          color="error"
          text
          @click="confirmDelete"
        >
          Delete
        </v-btn>

      </v-card-actions>

    </v-card>
  </v-dialog>
</template>

<script>
export default {
  name: "DeleteConfirmationDialog",

  props: {

    dialog: {
      type: Boolean,
      required: true
    },

    title: {
      type: String,
      default: "Delete"
    },

    message: {
      type: String,
      default: "Are you sure you want to delete?"
    }

  },

  methods: {

    closeDialog() {
      this.$emit("close");
    },

    confirmDelete() {
      this.$emit("confirm");
    }

  }

}
</script>


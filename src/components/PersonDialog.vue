<template>
  <v-dialog
    :value="dialog"
    max-width="600"
    persistent
    @input="closeDialog"
  >
    <v-card>

      <v-card-title>
        {{ dialogTitle }}
      </v-card-title>

      <v-divider></v-divider>

      <v-card-text>

        <v-form ref="personForm">

          <v-text-field
            label="Name"
            v-model="person.name"
            :rules="nameRules"
            outlined
            dense
          ></v-text-field>

          <v-textarea
            label="Bio"
            v-model="person.bio"
            outlined
            dense
            rows="3"
          ></v-textarea>

          <v-text-field
            label="Date of Birth"
            type="date"
            v-model="person.dateOfBirth"
            :rules="dateRules"
            outlined
            dense
          ></v-text-field>

          <v-select
            label="Gender"
            :items="genders"
            v-model="person.gender"
            :rules="genderRules"
            outlined
            dense
          ></v-select>

        </v-form>

      </v-card-text>

      <v-divider></v-divider>

      <v-card-actions>

        <v-spacer></v-spacer>

        <v-btn
          text
          @click="closeDialog"
        >
          Cancel
        </v-btn>

        <v-btn
          color="primary"
          @click="savePerson"
        >
          Save
        </v-btn>

      </v-card-actions>

    </v-card>
  </v-dialog>
</template>

<script>
export default {

  name: "PersonDialog",

  props: {

    dialog: {
      type: Boolean,
      required: true
    },

    entityType: {
      type: String,
      required: true
    }

  },

  data() {

    return {

      person: {

        name: "",

        bio: "",

        dateOfBirth: "",

        gender: ""

      },

      genders: [

        "Male",

        "Female",

        "Other"

      ],

      nameRules: [

        v => !!v || "Name is required"

      ],

      dateRules: [

        v => !!v || "Date of Birth is required"

      ],

      genderRules: [

        v => !!v || "Gender is required"

      ]

    }

  },

  computed: {

    dialogTitle() {

      return this.entityType === "actor"
        ? "Add Actor"
        : "Add Producer";

    }

  },

  methods: {

    savePerson() {

      if (!this.$refs.personForm.validate()) {
        return;
      }

      this.$emit("person-save", {
        entityType: this.entityType,
        person: { ...this.person }
      });

      this.resetForm();

    },

    closeDialog() {

      this.resetForm();

      this.$emit("close");

    },

    resetForm() {

      this.person = {

        name: "",

        bio: "",

        dateOfBirth: "",

        gender: ""

      };

      if (this.$refs.personForm) {
        this.$refs.personForm.resetValidation();
      }

    }

  }

}
</script>


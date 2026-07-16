<template>

  <v-app>

    <AppBar />

    <v-main>

      <router-view />

    </v-main>

  </v-app>

</template>

<script>

import AppBar from "@/components/appBar.vue";

export default {

  name: "App",

  components: {

    AppBar

  }

}

</script>

<style>

html,
body,
#app{

  height:100%;

}

</style>
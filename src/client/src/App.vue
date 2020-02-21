<template>
  <v-app id="app">
    <component :is="layout">
      <router-view />
    </component>
  </v-app>
</template>

<script>
export default {
  name: 'App',
  computed: {
    layout() {
      return this.$route.meta.layout || 'default';
    }
  }
};
</script>

<style>
#app,
body,
html,
.container-fluid {
  height: 100%;
}
</style>

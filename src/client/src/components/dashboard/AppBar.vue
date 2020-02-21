<template>
  <v-app-bar app clipped-left>
    <v-app-bar-nav-icon @click.stop="toggleDrawer"></v-app-bar-nav-icon>
    <v-toolbar-title>Bivrost</v-toolbar-title>
  </v-app-bar>
</template>

<script>
export default {
  methods: {
    toggleDrawer: function() {
      this.$emit('toggleDrawer');
    }
  }
};
</script>

<style scoped></style>

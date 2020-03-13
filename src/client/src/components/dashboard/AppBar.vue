<template>
  <v-app-bar app clipped-left>
    <v-app-bar-nav-icon @click.stop="toggleDrawer"></v-app-bar-nav-icon>
    <v-toolbar-title>Bivrost</v-toolbar-title>
    <v-spacer />
    <Connections />
  </v-app-bar>
</template>

<script>
import Connections from '@/components/dashboard/Connections.vue';

export default {
  components: {
    Connections
  },
  methods: {
    toggleDrawer: function() {
      this.$emit('toggleDrawer');
    }
  }
};
</script>

<style lang="scss" scoped></style>

<template>
  <div>
    <Drawer :drawer="drawer" />

    <AppBar @toggleDrawer="toggleDrawer" />

    <v-content>
      <slot />
    </v-content>
  </div>
</template>

<script>
import AppBar from '@/components/dashboard/AppBar.vue';
import Drawer from '@/components/dashboard/Drawer.vue';

export default {
  components: {
    AppBar,
    Drawer
  },
  data: () => ({
    drawer: false
  }),
  methods: {
    toggleDrawer: function() {
      this.drawer = !this.drawer;
    }
  }
};
</script>

<style scoped></style>

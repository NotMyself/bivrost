<template>
  <v-app id="app">
    <component :is="layout">
      <router-view />
    </component>
  </v-app>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  name: 'App',
  mounted: function() {
    this.connect('server');
    this.connect('chat');
    this.connect('obs');
  },
  methods: {
    ...mapActions('app', ['connect'])
  },
  computed: {
    layout() {
      return this.$route.meta.layout || 'default';
    }
  }
};
</script>

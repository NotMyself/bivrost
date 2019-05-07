<template>
  <div id="app">
    <app-nav />
    <router-view />
  </div>
</template>

<script>
import AppNav from '@/components/AppNav.vue';

export default {
  name: 'app',
  components: {
    AppNav
  },
  sockets: {
    receiveChatMessage(message) {
      this.$store.dispatch('chat/addMessage', message);
    },
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>

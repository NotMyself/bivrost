<template>
  <div class="text-center">
    <Indicator
      displayName="Server"
      resource="server"
      :connected="serverConnected"
    />

    <Indicator displayName="OBS" resource="obs" :connected="obsConnected" />

    <Indicator displayName="BOT" resource="chat" :connected="botConnected" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import Indicator from './Indicator.vue';

export default {
  components: { Indicator },
  computed: {
    ...mapGetters('app', ['botConnected', 'serverConnected', 'obsConnected'])
  }
};
</script>

<style scoped></style>

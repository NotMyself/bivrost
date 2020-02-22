<template>
  <div class="text-center">
    <v-tooltip bottom>
      <template v-slot:activator="{ on }">
        <v-chip class="ma-2" v-on="on">
          <v-avatar left>
            <v-icon :color="getColor(serverConnected)">mdi-antenna</v-icon>
          </v-avatar>
          Server
        </v-chip>
      </template>
      <span>Twitch Bot Status: {{ getStatus(serverConnected) }}</span>
    </v-tooltip>

    <v-tooltip bottom>
      <template v-slot:activator="{ on }">
        <v-chip class="ma-2" v-on="on">
          <v-avatar left>
            <v-icon :color="getColor(obsConnected)">mdi-antenna</v-icon>
          </v-avatar>
          OBS
        </v-chip>
      </template>
      <span>OBS Socket Status: {{ getStatus(obsConnected) }}</span>
    </v-tooltip>

    <v-tooltip bottom>
      <template v-slot:activator="{ on }">
        <v-chip class="ma-2" v-on="on">
          <v-avatar left>
            <v-icon :color="getColor(botConnected)">mdi-antenna</v-icon>
          </v-avatar>
          BOT
        </v-chip>
      </template>
      <span>BOT Socket Status: {{ getStatus(botConnected) }}</span>
    </v-tooltip>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
  computed: {
    ...mapGetters('app', ['botConnected', 'serverConnected', 'obsConnected'])
  },
  methods: {
    ...mapActions('app', ['connect']),
    getStatus: function(state) {
      switch (state) {
        case true:
          return 'Connected';
        case false:
          return 'Disconnected';
        default:
          return 'Unknown';
      }
    },
    getColor: function(state) {
      switch (state) {
        case true:
          return 'green';
        case false:
          return 'red';
        default:
          return 'grey';
      }
    }
  }
};
</script>

<style scoped></style>

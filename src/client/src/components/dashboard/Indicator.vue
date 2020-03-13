<template>
  <v-tooltip bottom>
    <template v-slot:activator="{ on }">
      <v-chip class="ma-2" v-on="on" @click.stop="() => connect(resource)">
        <v-avatar left>
          <v-icon :color="getColor(connected)">mdi-antenna</v-icon>
        </v-avatar>
        {{ displayName }}
      </v-chip>
    </template>
    <span>{{ displayName }} Status: {{ getStatus(connected) }}</span>
  </v-tooltip>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  props: {
    resource: {
      type: String,
      required: true
    },
    displayName: {
      type: String,
      required: true
    },
    connected: {
      type: Boolean,
      validator: prop => typeof prop === 'boolean' || prop === null
    }
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

<style lang="scss" scoped></style>

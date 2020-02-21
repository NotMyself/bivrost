<template>
  <v-navigation-drawer v-model="drawer" app clipped>
    <v-list dense>
      <v-list-item router :to="{ name: 'HomePage' }">
        <v-list-item-icon>
          <v-icon>mdi-view-dashboard</v-icon>
        </v-list-item-icon>
        <v-list-item-title>Dashboard</v-list-item-title>
      </v-list-item>

      <v-list-group prepend-icon="mdi-layers-triple">
        <template v-slot:activator>
          <v-list-item-title>Overlays</v-list-item-title>
        </template>
        <v-list-item router :to="{ name: 'ChatOverlayPage' }">
          <v-list-item-icon>
            <v-icon>mdi-chat</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Chat</v-list-item-title>
        </v-list-item>
      </v-list-group>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  props: {
    drawer: {
      type: Boolean,
      required: true
    }
  }
};
</script>

<style scoped></style>

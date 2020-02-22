<template>
  <v-container fill-height>
    <slot />
  </v-container>
</template>

<script>
export default {
  name: 'Overlay'
};
</script>

<style scoped>
div.container {
  margin-left: 5px;
  margin-right: 5px;
  padding: 0px;
  max-width: 1920px;
  max-height: 1080px;
}
</style>

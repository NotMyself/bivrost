<template>
  <b-container
    class="green-screen"
    fluid
  >
    <slot />
  </b-container>
</template>

<script>
export default {
  name: 'GreenScreen'
}
</script>

<style scoped>
  .green-screen {
    height: 100%;
  }
</style>


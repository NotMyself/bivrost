<template>
  <b-container class="green-screen" fluid>
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
    background-color: rgba(173,255,47,1);
    height: 100%;
  }
</style>


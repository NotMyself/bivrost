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
    background-color: rgba(0,0,0,0);
    height: 100%;
  }
</style>


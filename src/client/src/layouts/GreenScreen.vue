<template>
  <div class="container-fluid green-screen">
    <slot />
  </div>
</template>

<script>
export default {
  name: 'GreenScreen'
};
</script>

<style scoped>
.green-screen {
  height: 100%;
}
</style>

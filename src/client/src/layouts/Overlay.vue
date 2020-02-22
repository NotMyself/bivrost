<template>
  <div class="container-fluid green-screen">
    <slot />
  </div>
</template>

<script>
export default {
  name: 'Overlay'
};
</script>

<style scoped>
.green-screen {
  height: 100%;
}
</style>

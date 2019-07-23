<template>
  <div>
    <app-nav />
    <slot />
  </div>
</template>

<script>
import AppNav from '@/components/AppNav.vue';

export default {
  components: {
    AppNav
  }
};
</script>

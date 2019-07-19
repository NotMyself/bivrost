<template>
  <div>
    <template v-for="(word, i) in words">
      <img
        v-if="lookup[word]"
        :key="i"
        :src="lookup[word]"
      >
      <template v-else>
        {{ word }}
      </template>
    </template>
  </div>
</template>

<script>
export default {
  name: 'DisplayText',
  props: {
    emotes: {
      type: Array,
      default: () => []
    },
    message: {
      type: String,
      default: ''
    }
  },
  computed: {
    lookup() {
        return this.emotes.reduce((lookup, emote) => {
          lookup[emote.name] = emote.imageUrl;
          return lookup;
        }, {});
    },
    words() {
      return this.message.split(' ');
    }
  }
}
</script>

<style scoped>

</style>

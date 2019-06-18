<template>
  <div>
    <template v-for="(word, i) in words">
      <img :src="lookup[word]" v-bind:key="i"
            v-if="lookup[word]" />
      <template v-else> {{word}}</template>
    </template>
  </div>
</template>

<script>
export default {
  name: 'DisplayText',
  props: {
    emotes: Array,
    message: String
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

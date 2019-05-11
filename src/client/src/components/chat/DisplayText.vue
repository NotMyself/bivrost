<template>
  <div>
    <template v-for="(word, i) in words">
      <img :src="lookup[word]" v-bind:key="i"
            v-if="lookup[word]" />
      <template v-else>{{word}}</template>
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
        // eslint-disable-next-line no-empty-pattern
        let a = {};
        for (let i = 0; i < this.emotes.length; i++) {
          const emote = this.emotes[i];
          a[emote.name] = emote.imageUrl;
        }
        return a;
    },
    words() {
      return this.message.split(' ');
    }
  }
}
</script>

<style scoped>

</style>

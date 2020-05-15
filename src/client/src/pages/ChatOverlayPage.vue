<template>
  <v-container>
    <v-row>
      <v-col cols="3" offset="9" align-self="strech">
        <ChatList :chatMessages="displayMessages" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
import ChatList from '@/components/chat/ChatList.vue';

export default {
  name: 'ChatOverlayPage',
  components: {
    ChatList
  },
  computed: {
    ...mapGetters('chat', ['displayMessages'])
  }
};
</script>

<style scoped>
/* div.col {
  position: absolute;
  bottom: 0;
} */
</style>

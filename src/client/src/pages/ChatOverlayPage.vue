<template>
<b-row class="chat-display">
  <b-col cols="9"/>
  <b-col cols="3">
    <div class="chat-wrapper">
      <ChatList :chatMessages="displayMessages" />
    </div>
  </b-col>
</b-row>
</template>

<script>
import { mapState } from 'vuex'
import ChatList from '@/components/chat/ChatList.vue'

export default {
  name: 'chatOverlayPage',
  components:{
    ChatList
  },
  data: () => {
    return {
      maxDisplay: 20
    }
  },
  computed: mapState({
    displayMessages: function(state) {
      return state.chat.chatMessages.slice(
            Math.max(state.chat.chatMessages.length - this.maxDisplay, 0));
    }
  })
};
</script>

<style scoped>
.chat-display, .chat-wrapper {
  height: 100%;
}
</style>

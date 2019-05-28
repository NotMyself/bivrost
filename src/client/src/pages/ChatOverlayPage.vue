<template>
  <ChatList :chatMessages="displayMessages" />
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

</style>

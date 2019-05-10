<template>
<div class="chat-box">
  <ChatMessage v-for="(chatMessage, index) in chatMessages"
              v-bind:key="chatMessage.id"
              :chatMessage="chatMessage"
              :index="index"
              :count="chatMessages.length"
              :opacity="100" />
</div>
</template>

<script>
import ChatMessage from '@/components/chat/ChatMessage.vue';

export default {
  name: 'ChatList',
  components: {
    ChatMessage
  },
  props: {
    chatMessages: Array
  }
}
</script>

<style scoped>
.chat-box {
  height: 100%;
  overflow: hidden;
}

 .card-columns {
  column-count: 1;
}
</style>

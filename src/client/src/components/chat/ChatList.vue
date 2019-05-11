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
  position: absolute;
  right: 15px;
  width: 340px;
  bottom: 0;
  padding-right: 15px;
  padding-left: 25px;
}

.chat-box:after {
  position: absolute;
  content: "";
  left: 0px;
  top: 0px;
  height: 100%;
  width: 100%;
  background: linear-gradient(0deg, transparent, transparent 650px, rgba(173,255,47,1) 920px);
}
</style>

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
  width: 340px;
  height: 1080px;
  display: table-cell;
  vertical-align: bottom;
  overflow: hidden;
  padding-right: 15px;
  padding-left: 25px;
  padding-bottom: 70px
}

.chat-box:after {
  position: absolute;
  content: "";
  left: 0px;
  top: 0px;
  height: 100%;
  width: 100%;
  background: linear-gradient(0deg, rgba(0,0,0,0), rgba(0,0,0,0) 650px, rgba(0,0,0,1) 850px);
}
</style>

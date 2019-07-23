<template>
  <transition-group
    name="chat-box"
    class="chat-box"
    tag="div"
    enter-active-class="animated bounceInUp"
    leave-active-class="animated fadeOutRight"
  >
    <ChatMessage
      v-for="chatMessage in chatMessages"
      :key="chatMessage.id"
      :chat-message="chatMessage"
    />
  </transition-group>
</template>

<script>
import ChatMessage from '@/components/chat/ChatMessage.vue';

export default {
  name: 'ChatList',
  components: {
    ChatMessage
  },
  props: {
    chatMessages: {
      type: Array,
      default: () => []
    }
  }
};
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
  padding-bottom: 70px;
}

.chat-box-enter,
.chat-box-leave-to {
  opacity: 0;
}

.chat-box-move {
  transition: transform 0.5s;
}
</style>

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
div.chat-box {
  display: flex;
  flex-direction: column;
  align-content: bottom;
  align-content: flex-end;
}
</style>

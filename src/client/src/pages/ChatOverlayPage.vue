<template>
  <ChatList :chatMessages="displayMessages" />
</template>

<script>
import { mapGetters } from 'vuex'
import ChatList from '@/components/chat/ChatList.vue'

export default {
  name: 'chatOverlayPage',
  components:{
    ChatList
  },
  data: () => {
    return {
      maxDisplay: 5
    }
  },
  computed: {
    ...mapGetters('chat',[
      'displayMessages'
    ])
  }
};
</script>

<style scoped>

</style>

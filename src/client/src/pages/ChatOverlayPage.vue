<template>
  <div>
    <h3 class="text-center">Chat Overlay</h3>
    <hr/>
    <div class="chatWrapper">
        <div class="chatBox" v-for="message in chatMessages" v-bind:key="message.id">
          <div class="chatMessage">
            <div class="profile">
              <img class="profileImg" :src="message.user.profileImageUrl" />
            </div>
            <div class="body">
              <div class="bubble">
                <div class="io"/>
                <div class="cheer"/>
                <div class="moderator"/>
                <div class="message">
                  {{message.message}}
                </div>
                <div class="name">
                  {{message.user.displayName}}
                </div>
              </div>
            </div>
          </div>
        </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'chatOverlayPage',
  computed: mapState({
    chatMessages: state => state.chat.chatMessages
  })
};
</script>

<style scoped>

</style>

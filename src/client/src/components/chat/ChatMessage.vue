<template>
<b-card no-body :class="{
                  'chat-message': true,
                  'overflow-hidden': true,
                  'broadcaster': chatMessage.user.isBroadcaster,
                  'subscriber':chatMessage.user.isSubscriber,
                  'moderator': chatMessage.user.isModerator
                }"
                :style="{opacity}">
    <b-row no-gutters>
      <b-col md="12">
        <b-card-body>
          <b-card-img :src="chatMessage.user.profileImageUrl" />
          <b-card-title class="name">{{chatMessage.user.displayName}}</b-card-title>
          <b-card-sub-title v-if="chatMessage.user.isModerator">Moderator</b-card-sub-title>
          <b-card-text>
            {{chatMessage.message}}
          </b-card-text>
        </b-card-body>
        <b-card-footer v-if="chatMessage.bits">
          Cheer for {{chatMessage.bits}} bits
        </b-card-footer>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>

export default {
  name: 'ChatMessage',
  props: {
    chatMessage: Object,
    index: Number,
    count: Number
  },
  data: () => {
    return {
      opacity: () => {
        return 100;
      }
    }
  }
}
</script>

<style scoped>
.chat-message {
  background: rgba(66, 52, 78, 1);
  margin-top: 10px;
  margin-bottom: 10px;
}

.name {

}

img {
    width: 50px;
    border-radius: 25px;
    background-color: #3c3c3c;
    border-radius: 25px;
    border: solid 3px rgba(66, 52, 78, 1);
}
</style>

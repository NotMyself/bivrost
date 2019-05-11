<template>
  <b-card no-body :class="{
                  'chat-message': true,
                  'broadcaster': chatMessage.user.isBroadcaster,
                  'subscriber': chatMessage.user.isSubscriber,
                  'moderator': chatMessage.user.isModerator
                }">
    <b-card-body>
      <b-card-img :src="chatMessage.user.profileImageUrl" />
      <img class="watermark" :src="auth0" v-if="chatMessage.user.isBroadcaster" />
      <img class="watermark" :src="shield"
            v-if="chatMessage.user.isModerator
              && !chatMessage.user.isBroadcaster" />
      <img class="watermark" :src="sword"
            v-if="chatMessage.user.isSubscriber
              && !chatMessage.user.isBroadcaster
              && !chatMessage.user.isModerator" />
      <b-card-text>
        <EmoteMessage :emotes="chatMessage.emotes"
                      :message="chatMessage.message" />
      </b-card-text>
    <b-card-title>{{chatMessage.user.displayName}}</b-card-title>
    </b-card-body>
    <b-card-footer v-if="chatMessage.bits">
      Cheer for {{chatMessage.bits}} bits
    </b-card-footer>
  </b-card>
</template>

<script>
import EmoteMessage from "@/components/chat/EmoteMessage.vue"
import shield from "@/assets/shield.svg"
import sword from "@/assets/sword.svg"
import auth0 from "@/assets/auth0.svg"

export default {
  name: 'ChatMessage',
  props: {
    chatMessage: Object,
    index: Number,
    count: Number
  },
  components:{
    EmoteMessage
  },
  data: () => {
    return {
      shield,
      sword,
      auth0
    }
  }
}
</script>

<style scoped>
.card {
  border: solid 3px;
  border-radius: 20px 0px;
  border-color:  #EB5424;
  background-color: #F5F7F9;
}

.moderator {
  border-color: #16214D;
  background-color: #44C7F4;
}

.subscriber {
  border-color: #16214D;
  background-color: #E3E5E7;
}

.broadcaster {
  border-color: #44C7F4;
  background-color: #D0D2D3;
}

.card-img {
    width: 50px;
    position: absolute;
    left: -25px;
    bottom: -20px;
    border-radius: 25px;
    border: solid 3px;
    border-color: #EB5424;
}

.moderator .card-img {
  border-color: #16214D;
}

.subscriber .card-img {
  border-color: #16214D;
}

.broadcaster .card-img {
  border-color: #44C7F4;
}

.card-body {
  padding-top: 10px;
}

.chat-message {
  margin-top: 10px;
  margin-bottom: 20px;
}

.card-text {
  margin-top: 5px;
  margin-left: 15px;
  margin-right: 15px;
  margin-bottom: 15px;
}

.card-title {
  opacity: .54;
  font-weight: bold;
  margin:0;
  position: absolute;
  bottom: 3px;
  right: 15px;
}

.watermark {
  width: 25px;
  opacity: .25;
  position: absolute;
  right: 15px;
}

.broadcaster .watermark {
  opacity: 1;
  width: 40px;
  background-color: #F5F7F9;
  padding: 3px;
  right: -15px;
  top: -15px;
  border-radius: 10px;
  border: solid 3px;
  border-color: #44C7F4;
}
</style>

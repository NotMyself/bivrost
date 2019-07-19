<template>
  <b-card
    no-body
    :class="{
      'chat-message': true,
      'broadcaster': chatMessage.user.isBroadcaster,
      'subscriber': chatMessage.user.isSubscriber,
      'moderator': chatMessage.user.isModerator
    }"
  >
    <b-card-body>
      <b-card-img :src="chatMessage.user.profileImageUrl" />
      <img
        v-if="chatMessage.user.isBroadcaster"
        class="watermark"
        :src="auth0"
      >
      <img
        v-if="chatMessage.user.isModerator
          && !chatMessage.user.isBroadcaster"
        class="watermark"
        :src="shield"
      >
      <img
        v-if="chatMessage.user.isSubscriber
          && !chatMessage.user.isBroadcaster
          && !chatMessage.user.isModerator"
        class="watermark"
        :src="sword"
      >
      <b-card-text>
        <DisplayText
          :emotes="chatMessage.emotes"
          :message="chatMessage.message"
        />
      </b-card-text>
      <b-card-title>{{ chatMessage.user.displayName }}</b-card-title>
    </b-card-body>
    <b-card-footer v-if="chatMessage.bits">
      Cheer for {{ chatMessage.bits }} bits
    </b-card-footer>
  </b-card>
</template>

<script>
import DisplayText from "@/components/chat/DisplayText.vue"
import shield from "@/assets/shield.svg"
import sword from "@/assets/sword.svg"
import auth0 from "@/assets/auth0.svg"

export default {
  name: 'ChatMessage',
  components:{
    DisplayText
  },
  props: {
    chatMessage: {
      type: Object,
      default: () => {}
    }
  },
  data: () => {
    return {
      shield,
      sword,
      auth0,
      age: 100 * 1000,
      timeout: null
    }
  },
  mounted: function () {
    this.setTimeout();
  },
  methods: {
    remove: function () {
      this.$store.dispatch('chat/deleteMessage', this.chatMessage);
    },
    setTimeout: function () {
      this.timeout = setTimeout(() => {
        this.remove();
      }, this.age);
    }
  }
}
</script>

<style scoped>
.card {
  border: solid 3px;
  border-radius: 20px 0px;
  border-color:  #44C7F4;
  background-color: #F5F7F9;
}

.moderator {
  border-color: #5C666F;
}

.subscriber {
  border-color: #02B48F;
}

.broadcaster {
  border-color: #EB5423;
}

.card-img {
    width: 50px;
    position: absolute;
    left: -25px;
    bottom: -20px;
    border-radius: 25px;
    border: solid 3px;
    border-color: #44C7F4;
}

.moderator .card-img {
  border-color: #5C666F;
}

.subscriber .card-img {
  border-color: #02B48F;
}

.broadcaster .card-img {
  border-color: #EB5423;
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

.card-footer {
  background-color: #F0CC00;
  position: absolute;
  font-size: 12px;
  font-weight: bolder;
  color: #5C666F;
  top: 77px;
  right: 25px;
  padding-top: 0;
  padding-bottom: 0px;
}

div.card-footer {
  border-radius: 0px 0px 15px 15px;
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
  border-color: #EB5423;
}
</style>

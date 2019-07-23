<template>
  <div
    :class="{
      card: true,
      'chat-message': true,
      broadcaster: chatMessage.user.isBroadcaster,
      subscriber: chatMessage.user.isSubscriber,
      moderator: chatMessage.user.isModerator
    }"
  >
    <div class="card-body">
      <img class="card-img" :src="chatMessage.user.profileImageUrl" />
      <img
        v-if="chatMessage.user.isBroadcaster"
        class="watermark"
        :src="auth0"
      />
      <img
        v-if="chatMessage.user.isModerator && !chatMessage.user.isBroadcaster"
        class="watermark"
        :src="shield"
      />
      <img
        v-if="
          chatMessage.user.isSubscriber &&
            !chatMessage.user.isBroadcaster &&
            !chatMessage.user.isModerator
        "
        class="watermark"
        :src="sword"
      />
      <p class="card-text">
        <DisplayText
          :emotes="chatMessage.emotes"
          :message="chatMessage.message"
        />
      </p>
      <h4 class="card-title">{{ chatMessage.user.displayName }}</h4>
    </div>
    <div v-if="chatMessage.bits" class="card-footer">
      Cheer for {{ chatMessage.bits }} bits
    </div>
  </div>
</template>

<script>
import DisplayText from '@/components/chat/DisplayText.vue';
import shield from '@/assets/shield.svg';
import sword from '@/assets/sword.svg';
import auth0 from '@/assets/auth0.svg';

export default {
  name: 'ChatMessage',
  components: {
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
      age: 30 * 1000,
      timeout: null
    };
  },
  mounted: function() {
    this.setTimeout();
  },
  methods: {
    remove: function() {
      this.$store.dispatch('chat/deleteMessage', this.chatMessage);
    },
    setTimeout: function() {
      this.timeout = setTimeout(() => {
        this.remove();
      }, this.age);
    }
  }
};
</script>

<style scoped>
.card {
  border: solid 3px;
  border-radius: 20px 0px;
  border-color: #44c7f4;
  background-color: #f5f7f9;
}

.moderator {
  border-color: #5c666f;
}

.subscriber {
  border-color: #02b48f;
}

.broadcaster {
  border-color: #eb5423;
}

.card-img {
  width: 50px;
  position: absolute;
  left: -25px;
  bottom: -20px;
  border-radius: 25px;
  border: solid 3px;
  border-color: #44c7f4;
}

.moderator .card-img {
  border-color: #5c666f;
}

.subscriber .card-img {
  border-color: #02b48f;
}

.broadcaster .card-img {
  border-color: #eb5423;
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
  opacity: 0.54;
  font-weight: bold;
  margin: 0;
  position: absolute;
  bottom: 3px;
  right: 15px;
}

.card-footer {
  background-color: #f0cc00;
  position: absolute;
  font-size: 12px;
  font-weight: bolder;
  color: #5c666f;
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
  opacity: 0.25;
  position: absolute;
  right: 15px;
}

.broadcaster .watermark {
  opacity: 1;
  width: 40px;
  background-color: #f5f7f9;
  padding: 3px;
  right: -15px;
  top: -15px;
  border-radius: 10px;
  border: solid 3px;
  border-color: #eb5423;
}
</style>

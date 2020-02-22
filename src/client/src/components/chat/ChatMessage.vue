<template>
  <v-card
    :class="{
      broadcaster: chatMessage.user.isBroadcaster,
      subscriber: chatMessage.user.isSubscriber,
      moderator: chatMessage.user.isModerator
    }"
    outlined
    shaped
  >
    <v-list-item>
      <v-list-item-avatar size="50" color="grey">
        <v-img
          height="50"
          width="50"
          :src="chatMessage.user.profileImageUrl"
        ></v-img>
      </v-list-item-avatar>
      <v-list-item-content>
        <v-card-text>
          <DisplayText
            :emotes="chatMessage.emotes"
            :message="chatMessage.message"
          />
        </v-card-text>
        <v-list-item-title class="headline mb-1">
          {{ chatMessage.user.displayName }}
        </v-list-item-title>
      </v-list-item-content>
    </v-list-item>
  </v-card>
  <!-- <div
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
  </div> -->
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
    //this.setTimeout();
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
div.v-card {
  width: 100%;
  margin-bottom: 5px;
}

div.headline {
  text-align: right;
}
div.v-avatar {
  border: solid 1px #02b48f;
}

.moderator {
  border-color: #5c666f;
}
.subscriber {
  border-color: #02b48f;
}
.broadcaster {
  border-color: #eb5423 !important;
}
</style>

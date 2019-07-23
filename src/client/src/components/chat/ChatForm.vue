<template>
  <div></div>
</template>

<script>
export default {
  name: 'ChatForm',
  data: () => {
    return {
      user: {
        id: '109912417',
        displayName: 'IAmNotMyself',
        profileImageUrl:
          'https://static-cdn.jtvnw.net/jtv_user_pictures/20bd3f0a-ce68-4f5c-a9bf-f61b950be3d2-profile_image-300x300.png',
        isBroadcaster: true,
        isModerator: false,
        isSubscriber: true,
        badges: [
          { key: 'broadcaster', value: '1' },
          { key: 'subscriber', value: '0' },
          { key: 'premium', value: '1' }
        ],
        bits: 0
      },
      id: '8f531be8-6acf-4d93-bd9d-5cfdec98a703',
      message: 'test',
      emotes: [],
      bits: 0,
      roomId: '109912417'
    };
  }
};
</script>

<style lang="scss" scoped></style>

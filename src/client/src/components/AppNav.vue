<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light fixed-top">
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarTogglerDemo01" aria-controls="navbarTogglerDemo01" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarTogglerDemo01">
      <router-link :to="{ name: 'HomePage' }" class="navbar-brand">Bivrost</router-link>
      <ul class="navbar-nav mr-auto mt-2 mt-lg-0">
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="overlaysMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Overlays
          </a>
          <div class="dropdown-menu" aria-labelledby="overlaysMenuLink">
            <router-link :to="{ name: 'ChatOverlayPage' }" class="nav-link">Chat</router-link>
          </div>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'app-nav',
  components: {
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>

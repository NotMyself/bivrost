<template>
  <b-navbar toggleable="md" type="light" variant="light">
      <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
      <b-navbar-brand to="/">Bivrost</b-navbar-brand>
      <b-collapse is-nav id="nav_collapse">
        <b-navbar-nav>
        <b-nav-item-dropdown text="Overlay">
          <b-dropdown-item :to="{ name: 'ChatOverlayPage' }">Chat</b-dropdown-item>
        </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
</template>

<script>
export default {
  name: 'app-nav'
};
</script>

<style scoped>

</style>

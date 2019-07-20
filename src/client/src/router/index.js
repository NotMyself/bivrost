import Vue from 'vue';
import Router from 'vue-router';

import ChatOverlayPage from '@/pages/ChatOverlayPage.vue';
import HomePage from '@/pages/HomePage.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'HomePage',
      component: HomePage
    },
    {
      path: '/overlays/chat',
      name: 'ChatOverlayPage',
      meta: { layout: 'green-screen' },
      component: ChatOverlayPage
    }
  ]
});

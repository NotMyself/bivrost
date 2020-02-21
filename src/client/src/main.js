import Vue from 'vue';
import router from './router';
import store from './store';
import App from './App.vue';
import vuetify from './plugins/vuetify';

import Default from '@/layouts/Default.vue';
import GreenScreen from '@/layouts/GreenScreen.vue';

Vue.component('default', Default);
Vue.component('green-screen', GreenScreen);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app');

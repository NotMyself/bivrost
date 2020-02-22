import Vue from 'vue';
import router from './router';
import store from './store';
import App from './App.vue';
import vuetify from './plugins/vuetify';

import Default from '@/layouts/Default.vue';
import Overlay from '@/layouts/Overlay.vue';

Vue.component('default', Default);
Vue.component('overlay', Overlay);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app');

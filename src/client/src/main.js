import Vue from 'vue'
import VueSignalR from './services/signalr'

import router from './router';
import store from './store';

import App from './App.vue'


Vue.config.productionTip = false

Vue.use(VueSignalR, '/client-hub');

new Vue({
  router,
  store,
  render: h => h(App),
  created() {
    this.$socket.start({
      log: false
    });
  },
}).$mount('#app');

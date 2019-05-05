import Vue from 'vue'
import VueSignalR from './services/signalr'
import App from './App.vue'

Vue.config.productionTip = false

Vue.use(VueSignalR, '/client-hub');

new Vue({
  render: h => h(App),
  created() {
    this.$socket.start({
      log: true // Active only in development for debugging.
    });
  },
}).$mount('#app')

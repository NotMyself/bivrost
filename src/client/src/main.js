import Vue from 'vue';
import router from './router';
import store from './store';
import App from './App.vue';
import Default from '@/layouts/Default.vue';
import GreenScreen from '@/layouts/GreenScreen.vue';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'animate.css/animate.min.css';

Vue.component('default-layout', Default);
Vue.component('green-screen-layout', GreenScreen);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');

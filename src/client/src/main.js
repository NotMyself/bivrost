import Vue from "vue";
import VueSignalR from "./services/signalr";
import router from "./router";
import store from "./store";
import App from "./App.vue";
import Default from "@/layouts/Default.vue";
import GreenScreen from "@/layouts/GreenScreen.vue";
import BootstrapVue from "bootstrap-vue";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

Vue.component("default-layout", Default);
Vue.component("green-screen-layout", GreenScreen);

Vue.use(BootstrapVue);
Vue.use(VueSignalR, "/client-hub");

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App),
  created() {
    this.$socket.start({
      log: false
    });
  }
}).$mount("#app");

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
import "animate.css/animate.min.css";

Vue.component("default-layout", Default);
Vue.component("green-screen-layout", GreenScreen);

Vue.use(BootstrapVue);
Vue.use(VueSignalR, "/client-hub");

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  created() {
    this.$socket.start({
      log: false
    });
  },
  render: h => h(App)
}).$mount("#app");

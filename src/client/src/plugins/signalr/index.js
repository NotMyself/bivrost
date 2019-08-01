import Hub from './signalr';

export default {
  install(Vue) {
    Hub.start();
    Vue.prototype.$socket = Hub.client;
  }
};

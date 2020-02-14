import Vue from 'vue';
import Vuex from 'vuex';

import chat from './modules/chat';
import obs from './modules/obs';

import botSocket from './plugins/bot';
import obsSocket from './plugins/obs';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    chat,
    obs
  },
  plugins: [botSocket(), obsSocket()]
});

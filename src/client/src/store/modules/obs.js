const state = {
  connected: false,
  error: null
};

const getters = {
  isConnected: state => state.connected
};

const actions = {
  connectionOpened({ commit }) {
    commit('SET_CONNECTION', true);
  },
  connectionClosed({ commit }) {
    commit('SET_CONNECTION', false);
  },
  connectionError({ commit }, error) {
    commit('SET_ERROR', error);
  },
  sendMessage({ commit }, message) {
    commit('SEND_MESSAGE', message);
  }
};

const mutations = {
  SET_CONNECTION(state, message) {
    state.connected = message;
  },
  SET_ERROR(state, error) {
    state.error = error;
  },
  // eslint-disable-next-line no-unused-vars
  SEND_MESSAGE(state, message) {}
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

const state = {
  bot: null,
  obs: null,
  server: null,
  errors: []
};

const getters = {
  botConnected: state => state.bot,
  obsConnected: state => state.obs,
  serverConnected: state => state.server,
  errors: state => state.errors
};

const actions = {
  connect({ commit }, type) {
    commit(`${type}/CONNECT`, type, { root: true });
  },
  connectionOpened({ commit }, type) {
    commit('CONNECTION_OPENED', type);
  },
  connectionClosed({ commit }, type) {
    commit('CONNECTION_CLOSED', type);
  },
  connectionError({ commit }, error) {
    commit('ADD_ERROR', error);
  }
};

const mutations = {
  CONNECTION_OPENED(state, type) {
    state[type] = true;
  },
  CONNECTION_CLOSED(state, type) {
    state[type] = false;
  },
  ADD_ERROR(state, error) {
    state.errors.push(error);
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

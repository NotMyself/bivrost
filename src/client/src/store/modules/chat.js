const state = {
  connected: false,
  error: null,
  chatMessages: [],
  limit: 5
};

const getters = {
  displayMessages: state => state.chatMessages
};

const actions = {
  addMessage({ commit }, message) {
    commit('ADD_MESSAGE', message);
  },
  deleteMessage({ commit }, message) {
    commit('DELETE_MESSAGE', message);
  },
  connectionOpened({ commit }) {
    commit('SET_CONNECTION', true);
  },
  connectionClosed({ commit }) {
    commit('SET_CONNECTION', false);
  },
  connectionError({ commit }, error) {
    commit('SET_ERROR', error);
  }
};

const mutations = {
  ADD_MESSAGE(state, message) {
    while (state.chatMessages.length >= state.limit) {
      state.chatMessages.shift();
    }
    state.chatMessages.push(message);
  },
  DELETE_MESSAGE(state, message) {
    state.chatMessages = state.chatMessages.filter(m => m.id !== message.id);
  },
  SET_CONNECTION(state, message) {
    state.connected = message;
  },
  SET_ERROR(state, error) {
    state.error = error;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

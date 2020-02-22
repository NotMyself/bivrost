const state = {
  messages: [],
  limit: 5
};

const getters = {
  displayMessages: state => state.messages
};

const actions = {
  addMessage({ commit }, message) {
    commit('ADD_MESSAGE', message);
  },
  deleteMessage({ commit }, message) {
    commit('DELETE_MESSAGE', message);
  }
};

const mutations = {
  ADD_MESSAGE(state, message) {
    while (state.messages.length >= state.limit) {
      state.messages.shift();
    }
    state.messages.push(message);
  },
  DELETE_MESSAGE(state, message) {
    state.messages = state.messages.filter(m => m.id !== message.id);
  },
  // eslint-disable-next-line no-unused-vars
  CONNECT(state, message) {}
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

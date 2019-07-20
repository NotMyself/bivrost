const state = {
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
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

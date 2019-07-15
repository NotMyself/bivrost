const state = {
  chatMessages: []
};

const getters = {};

const actions = {
  addMessage({ commit }, message) {
    commit("ADD_MESSAGE", message);
  },
  deleteMessage({ commit }, message) {
    commit("DELETE_MESSAGE", message);
  }
};

const mutations = {
  ADD_MESSAGE(state, message) {
    state.chatMessages.push(message);
  },
  DELETE_MESSAGE(state, message) {
    const index = state.chatMessages.findIndex(m => m.id === message.id);
    state.chatMessages.splice(index, 1);
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

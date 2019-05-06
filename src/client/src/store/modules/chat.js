

const state = {
  chatMessages: [],
}

const getters = {
  getMessages(state) {
    return state.chatMessages;
  }
}

const actions = {
  addMessage({ commit }, message) {
    commit('addMessage', message);
  }
}

const mutations = {
  addMessage(state, message) {
    state.items.push(message);
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}

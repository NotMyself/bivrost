

const state = {
  chatMessages: [],
}

const getters = {

}

const actions = {
  addMessage({ commit }, message) {
    commit('addMessage', message);
  }
}

const mutations = {
  addMessage(state, message) {
    // eslint-disable-next-line no-console
    console.dir(message);
    state.chatMessages = [...state.chatMessages, message];
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}

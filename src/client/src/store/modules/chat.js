

const state = {
  chatMessages: [],
}

const getters = {

}

const actions = {
  addMessage({ commit }, message) {
    commit('ADD_MESSAGE', message);
  }
}

const mutations = {
  ADD_MESSAGE(state, message) {
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

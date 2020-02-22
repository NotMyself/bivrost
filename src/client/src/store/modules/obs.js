const state = {};

const getters = {};

const actions = {
  sendMessage({ commit }, message) {
    commit('SEND_MESSAGE', message);
  }
};

const mutations = {
  // eslint-disable-next-line no-unused-vars
  SEND_MESSAGE(state, message) {},
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

import { createStore } from 'vuex';

export default createStore({
  state: {
    isLoggedIn: true,
  },
  mutations: {
    signIn(state) {
      state.isLoggedIn = true;
    },
    signOut(state) {
      state.isLoggedIn = false;
    },
  },
  actions: {
    signIn(context) {
      context.commit('signIn');
    },
    signOut(context) {
      context.commit('signOut');
    },
  },
  modules: {
  },
  getters: {
    isLoggedIn: (state) => state.isLoggedIn,
  },
});

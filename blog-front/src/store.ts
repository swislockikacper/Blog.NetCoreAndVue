import Vue from "vue";
import Vuex from "vuex";
import router from "./router";
import axios from "axios";

Vue.use(Vuex);

const types = {
  LOGIN: "LOGIN",
  LOGOUT: "LOGOUT"
};

export default new Vuex.Store({
  state: {
    logged: localStorage.getItem("token") !== null ? true : false
  },
  getters: {
    isLogged: state => state.logged
  },
  mutations: {
    [types.LOGIN](state) {
      state.logged = true;
    },

    [types.LOGOUT](state) {
      state.logged = false;
    }
  },
  actions: {
    login({ commit }, credential) {
      axios
        .post("https://localhost:44324/api/user/login", credential)
        .then(response => response.data.json())
        .then(result => {
          localStorage.setItem("token", result.token);
          commit(types.LOGIN);
          router.push({ path: "/home" });
        });
    },

    logout({ commit }) {
      localStorage.removeItem("token");
      commit(types.LOGOUT);
      router.push({ path: "/login" });
    }
  }
});

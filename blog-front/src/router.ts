import Vue from "vue";
import Router from "vue-router";
import Login from "@/components/Login.vue";
import Register from "@/components/Register.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/login",
      name: "login",
      component: Login
    },
    {
      path: "/logout",
      name: "logout",
      component: Login
    },
    {
      path: "/register",
      name: "register",
      component: Register
    }
  ]
});

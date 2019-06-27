<template>
  <div id="app">
    <Register/>
    <!-- <Login/> -->
  </div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import Register from "./components/Register.vue";
import Login from "./components/Login.vue";

@Component({
  components: { Register, Login }
})
export default class App extends Vue {}
</script>
<style lang="scss">
</style>

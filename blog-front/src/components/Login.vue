<template>
  <div class="container">
    <h2>Zaloguj się</h2>

    <form action>
      <div class="container">
        <label for="uname">
          <b>Email</b>
        </label>
        <input type="text" placeholder="Wpisz Email" name="uname" required>

        <label for="psw">
          <b>Hasło</b>
        </label>
        <input type="password" placeholder="Wpisz hasło" name="psw" required>

        <button type="submit">Zaloguj się</button>
      </div>
    </form>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class Login extends Vue {}
</script>

<style scoped lang="scss">
.container {
  width: 70%;
  margin: 0 auto;

  h2 {
    margin-left: 15%;
  }

  form {
    .container {
      input {
        width: 100%;
        padding: 1rem;
        margin: 0.5rem 0;
        border: none;
        background: #f1f1f1;
      }

      input:focus {
        background-color: #ddd;
      }

      button {
        background-color: #808681;
        color: white;
        padding: 1rem 1.5rem;
        margin: 0 auto;
        border: none;
        cursor: pointer;
        width: 10rem;
        opacity: 0.9;
      }

      button:hover {
        opacity: 1;
      }
    }
  }
}
</style>

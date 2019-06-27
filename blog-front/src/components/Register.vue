<template>
  <div class="container">
    <section>
      <div class="container">
        <h1>Zarejestruj się</h1>
        <p>Wypełnij formularz, aby założyć konto.</p>

        <label for="fullName">
          <b>Imię i nazwisko</b>
        </label>
        <input
          type="text"
          placeholder="Wpisz imię i nazwisko"
          name="fullName"
          v-model="user.fullName"
          required
        >

        <label for="email">
          <b>Email</b>
        </label>
        <input type="text" placeholder="Wpisz Email" name="email" v-model="user.email" required>

        <label for="psw">
          <b>Hasło</b>
        </label>
        <input
          type="password"
          placeholder="Wpisz hasło"
          name="psw"
          v-model="user.password"
          required
        >

        <label for="psw-repeat">
          <b>Powtórz hasło</b>
        </label>
        <input
          type="password"
          placeholder="Powtórz hasło"
          name="psw-repeat"
          v-model="user.repeatedPassword"
          required
        >

        <button v-on:click="addUser">Zarejestruj się</button>
      </div>
    </section>
    <p class="error">{{ message }}</p>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import axios from "axios";
import User from "../models/user";

@Component
export default class Register extends Vue {
  user: User = new User("", "", "", "");
  message: string = "";

  addUser() {
    if (this.user.fullName == "") {
      this.message = "Imię i nazwisko nie może być puste";
    } else if (this.user.email == "") {
      this.message = "Email nie może być pusty";
      return;
    } else if (
      this.user.password != this.user.repeatedPassword ||
      this.user.password == ""
    ) {
      this.message = "Wpisane hasła się różnią, lub są za krótkie";
      return;
    }
  }
}
</script>

<style scoped lang="scss">
.container {
  width: 70%;
  margin: 0 auto;

  section {
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
        padding: 1rem 1rem;
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

  .error {
    margin-left: 15%;
    font-weight: 600;
    color: #d41212;
  }
}
</style>

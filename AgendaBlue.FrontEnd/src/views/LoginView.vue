<template>
  <div class="login-container">
    <div class="login-box">
      <h1>Login</h1>
      <form @submit.prevent="handleLogin" class="login-form">
        <div class="form-group">
          <label for="username">Usuário</label>
          <input
            type="text"
            id="username"
            v-model="username"
            placeholder="Digite seu usuário"
            required
          />
        </div>

        <div class="form-group">
          <label for="password">Senha</label>
          <input
            type="password"
            id="password"
            v-model="password"
            placeholder="Digite sua senha"
            required
          />
        </div>

        <button type="submit" class="login-button">Fazer Login</button>

        <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import axios from 'axios'

export default defineComponent({
  name: 'LoginView',
  setup() {
    const username = ref('')
    const password = ref('')
    const errorMessage = ref('')

    const handleLogin = async () => {
      try {
        // Chama a API de login
        const response = await axios.post('https://localhost:7189/User/Login', {
          email: username.value,
          password: password.value,
        })

        if (response.data.token) {
          localStorage.setItem('idUser', response.data.idUser)
          localStorage.setItem('authToken', response.data.token)
          localStorage.setItem('userName', response.data.userName)
          // Redireciona para a página inicial após o login
          window.location.href = ''
        }
      } catch {
        // Exibe uma mensagem de erro caso o login falhe
        errorMessage.value = 'Usuário ou senha inválidos. Tente novamente!'
      }
    }

    return {
      username,
      password,
      errorMessage,
      handleLogin,
    }
  },
})
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f4f4f4;
}

.login-box {
  background-color: #fff;
  padding: 40px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 300px;
  text-align: center;
}

h1 {
  margin-bottom: 20px;
  font-size: 24px;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
  text-align: left;
}

label {
  font-size: 14px;
  color: #555;
}

input {
  width: 100%;
  padding: 10px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 16px;
}

button {
  width: 100%;
  padding: 10px;
  margin-top: 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}

.error-message {
  margin-top: 15px;
  color: #ff4d4f;
  font-size: 14px;
}
</style>

<template>
  <div class="professionals-view">
    <h1>Profissionais</h1>
    <p>Gerencie os profissionais cadastrados.</p>
    <div class="actions">
      <button class="btn btn-secondary" @click="getAllShavys">Ver Todos</button>
    </div>

    <!-- Lista de Professionals -->
    <div v-if="shavys.length > 0" class="Professionals-list">
      <h2>Lista de Profissionais</h2>
      <ul>
        <li v-for="shavy in shavys" :key="shavy.id">
          <strong>{{ shavy.id }}</strong> {{ shavy.barberName }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue'

export default {
  name: 'ProfessionalsView',
  setup() {
    const shavys = ref([])

    const getAllShavys = async () => {
      try {
        const token = localStorage.getItem('authToken')
        if (!token) {
          alert('Usuário não autenticado.')
          return
        }
        const response = await fetch('https://localhost:7189/User/GetAllShavys', {
          method: 'GET',
          headers: {
            Authorization: `Bearer ${token}`, // Adiciona o token ao cabeçalho
            'Content-Type': 'application/json',
          },
        })

        if (response.ok) {
          shavys.value = await response.json()
        } else {
          alert('Erro ao buscar os Profissionais.')
        }
      } catch (error) {
        console.error('Erro:', error)
        alert('Erro ao conectar à API.')
      }
    }

    onMounted(getAllShavys)

    return {
      shavys,
      getAllShavys,
    }
  },
}
</script>

<style scoped>
.professionals-view {
  text-align: center;
  margin: 20px;
  padding: 20px;
  background-color: #f9fafb;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

h1 {
  font-size: 2.5rem;
  color: #2d3748;
}

p {
  font-size: 1.2rem;
  color: #718096;
}

.actions {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  gap: 15px;
}

.btn {
  padding: 10px 20px;
  font-size: 1rem;
  font-weight: 500;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.3s ease-in-out;
}

.btn-primary {
  background-color: #38a169;
  color: white;
}

.btn-primary:hover {
  background-color: #2f855a;
}

.btn-secondary {
  background-color: #edf2f7;
  color: #2d3748;
}

.btn-secondary:hover {
  background-color: #e2e8f0;
}

.form-container {
  margin-top: 20px;
  padding: 20px;
  background-color: #ffffff;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 15px;
  text-align: left;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  color: #4a5568;
}

input,
textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #cbd5e0;
  border-radius: 5px;
  font-size: 1rem;
}

input:focus,
textarea:focus {
  outline: none;
  border-color: #38a169;
}

.Professionals-list {
  margin-top: 30px;
  text-align: left;
}

.Professionals-list h2 {
  font-size: 1.8rem;
  color: #2d3748;
  margin-bottom: 10px;
}

.Professionals-listul {
  list-style: none;
  padding: 0;
}

.Professionals-list li {
  background-color: #f7fafc;
  margin-bottom: 10px;
  padding: 10px;
  border: 1px solid #e2e8f0;
  border-radius: 5px;
}
</style>

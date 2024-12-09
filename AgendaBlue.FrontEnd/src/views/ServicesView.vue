<template>
  <div class="services-view">
    <!-- Título -->
    <h1>Serviços</h1>
    <p>Gerencie os serviços disponíveis.</p>

    <!-- Ações -->
    <div class="actions">
      <button class="btn btn-primary" @click="showForm = !showForm">Novo Serviço</button>
      <button class="btn btn-secondary" @click="getAllServices()">Ver Todos</button>
    </div>
    <!-- Lista de Serviços -->
    <div v-if="services.length > 0" class="services-list">
      <h2>Lista de Serviços</h2>
      <ul>
        <li v-for="service in services" :key="service.id">
          <strong>{{ service.id }}</strong> {{ service.nameServices }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue'

export default {
  name: 'ServicesView',
  setup() {
    const showForm = ref(false)
    const services = ref([])

    // Função para buscar todos os serviços
    const getAllServices = async () => {
      try {
        const token = localStorage.getItem('authToken')
        if (!token) {
          alert('Usuário não autenticado.')
          return
        }
        const response = await fetch('https://localhost:7189/User/GetAllServices', {
          method: 'GET',
          headers: {
            Authorization: `Bearer ${token}`, // Adiciona o token ao cabeçalho
            'Content-Type': 'application/json',
          },
        })
        if (response.ok) {
          services.value = await response.json()
        } else {
          alert('Erro ao buscar os serviços.')
        }
      } catch (error) {
        console.error('Erro:', error)
        alert('Erro ao conectar à API.')
      }
    }

    onMounted(getAllServices)

    return {
      showForm,
      services,
      getAllServices,
    }
  },
}
</script>

<style scoped>
.services-view {
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

.services-list {
  margin-top: 30px;
  text-align: left;
}

.services-list h2 {
  font-size: 1.8rem;
  color: #2d3748;
  margin-bottom: 10px;
}

.services-list ul {
  list-style: none;
  padding: 0;
}

.services-list li {
  background-color: #f7fafc;
  margin-bottom: 10px;
  padding: 10px;
  border: 1px solid #e2e8f0;
  border-radius: 5px;
}
</style>

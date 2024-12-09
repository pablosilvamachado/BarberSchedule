<template>
  <div class="flex flex-col items-center justify-center min-h-screen bg-gray-100">
    <!-- Título Principal -->
    <h1 class="text-4xl font-bold text-gray-800 mb-10">Dashboard</h1>

    <!-- Container de Cards -->
    <div class="grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3">
      <!-- Card: Agendamento -->
      <div
        class="w-64 p-6 bg-white rounded-lg shadow-md hover:shadow-xl cursor-pointer transform hover:scale-105 transition duration-300"
        @click="navigateTo('/schedule')"
      >
        <h2 class="text-xl font-semibold text-gray-700">Agendamentos</h2>
        <p class="mt-2 text-gray-500">Gerencie e visualize todos os agendamentos.</p>
      </div>

      <!-- Card: Profissionais -->
      <div
        class="w-64 p-6 bg-white rounded-lg shadow-md hover:shadow-xl cursor-pointer transform hover:scale-105 transition duration-300"
        @click="navigateTo('/professionals')"
      >
        <h2 class="text-xl font-semibold text-gray-700">Profissionais</h2>
        <p class="mt-2 text-gray-500">Adicione, edite ou remova profissionais.</p>
      </div>

      <!-- Card: Clientes -->
      <div
        class="w-64 p-6 bg-white rounded-lg shadow-md hover:shadow-xl cursor-pointer transform hover:scale-105 transition duration-300"
        @click="navigateTo('/clients')"
      >
        <h2 class="text-xl font-semibold text-gray-700">Clientes</h2>
        <p class="mt-2 text-gray-500">Gerencie os dados dos clientes.</p>
      </div>

      <!-- Card: Serviços -->
      <div
        class="w-64 p-6 bg-white rounded-lg shadow-md hover:shadow-xl cursor-pointer transform hover:scale-105 transition duration-300"
        @click="navigateTo('/services')"
      >
        <h2 class="text-xl font-semibold text-gray-700">Serviços</h2>
        <p class="mt-2 text-gray-500">Adicione ou edite os serviços oferecidos.</p>
      </div>

      <!-- Card: Horários -->
      <div
        class="w-64 p-6 bg-white rounded-lg shadow-md hover:shadow-xl cursor-pointer transform hover:scale-105 transition duration-300"
        @click="navigateTo('/schedule-settings')"
      >
        <h2 class="text-xl font-semibold text-gray-700">Horários</h2>
        <p class="mt-2 text-gray-500">Configure os horários disponíveis para agendamentos.</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'HomeView',
  setup() {
    // Função para navegar para as rotas correspondentes
    const navigateTo = (route: string) => {
      window.location.href = route // Navegação direta
    }

    return {
      navigateTo,
    }
  },
})
</script>

<style scoped>
/* Background geral */
.bg-gray-100 {
  background-color: #f9fafb; /* Cinza claro */
}

/* Título principal */
h1 {
  font-family: 'Roboto', sans-serif; /* Fonte limpa e profissional */
  color: #1a202c; /* Azul escuro */
  text-align: center;
  margin-bottom: 2rem;
}

/* Cards */
.grid {
  display: grid;
  gap: 1.5rem;
}

@media (min-width: 768px) {
  .grid {
    grid-template-columns: repeat(2, 1fr); /* 2 colunas no tablet */
  }
}

@media (min-width: 1024px) {
  .grid {
    grid-template-columns: repeat(3, 1fr); /* 3 colunas no desktop */
  }
}

.w-64 {
  width: 100%; /* Ajuste automático para ocupar espaço */
  max-width: 18rem; /* Largura máxima */
  padding: 1.5rem;
  background: #fff;
  border-radius: 0.75rem;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease-in-out;
  cursor: pointer;
}

.w-64:hover {
  box-shadow: 0 6px 8px rgba(0, 0, 0, 0.15);
  transform: scale(1.05);
}

/* Títulos dos Cards */
h2 {
  font-family: 'Roboto', sans-serif;
  font-size: 1.25rem;
  color: #2d3748; /* Cinza escuro */
  margin-bottom: 0.5rem;
}

/* Texto dos Cards */
p {
  font-family: 'Roboto', sans-serif;
  font-size: 1rem;
  color: #718096; /* Cinza médio */
}

/* Responsividade geral */
@media (max-width: 768px) {
  .min-h-screen {
    padding: 1rem; /* Margem extra em dispositivos menores */
  }
}
</style>

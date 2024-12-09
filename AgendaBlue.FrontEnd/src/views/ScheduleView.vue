<template>
  <div class="schedule-view">
    <!-- Título -->
    <h1>Agendamentos</h1>
    <p>Gerencie os agendamentos de clientes.</p>

    <!-- Ações -->
    <div class="actions">
      <button class="btn btn-secondary" @click="getAllScheduling">Ver Todos</button>
    </div>

    <!-- Lista de Agendamentos -->
    <div v-if="schedulings.length > 0" class="schedulings-list">
      <h2>Agendamentos</h2>
      <ul>
        <li v-for="scheduling in schedulings" :key="scheduling.idSchedulling">
          <strong>{{ scheduling.nameUser }}</strong> - {{ scheduling.hairCurtDate }} <br />
          Serviço Desejado: {{ scheduling.desiredService }} <br />
          Horário: {{ scheduling.time }} <br />
          <span v-if="scheduling.schedulingCompleted">Agendamento Concluído</span>
          <span v-else>Agendamento Pendente</span>
        </li>
      </ul>
    </div>

    <!-- Mensagem de Sem Agendamentos -->
    <div v-else>
      <p>Nenhum agendamento encontrado.</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'ScheduleView',
  data() {
    return {
      schedulings: [],
    }
  },
  methods: {
    async getAllScheduling() {
      const token = localStorage.getItem('authToken') // Pega o token armazenado no localStorage

      if (!token) {
        alert('Usuário não autenticado')
        return
      }

      try {
        const response = await fetch('https://localhost:7189/User/GetAllScheduling', {
          method: 'GET',
          headers: {
            Authorization: `Bearer ${token}`, // Adiciona o token ao cabeçalho
            'Content-Type': 'application/json',
          },
        })

        if (!response.ok) {
          throw new Error('Erro ao carregar agendamentos')
        }

        const data = await response.json()
        this.schedulings = data.schedulings // Atualiza a lista de agendamentos com os dados da resposta
      } catch (error) {
        console.error(error)
        alert('Erro ao obter agendamentos')
      }
    },
  },
})
</script>

<style scoped>
.schedule-view {
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
}

.btn-secondary {
  padding: 10px 20px;
  background-color: #edf2f7;
  color: #2d3748;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-secondary:hover {
  background-color: #e2e8f0;
}

.schedulings-list {
  margin-top: 30px;
  text-align: left;
}

.schedulings-list h2 {
  font-size: 1.8rem;
  color: #2d3748;
}

.schedulings-list ul {
  list-style: none;
  padding: 0;
}

.schedulings-list li {
  background-color: #f7fafc;
  margin-bottom: 10px;
  padding: 15px;
  border: 1px solid #e2e8f0;
  border-radius: 5px;
}

.schedulings-list span {
  font-size: 1.1rem;
  color: #38a169;
}
</style>

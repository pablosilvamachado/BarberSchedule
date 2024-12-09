<template>
  <div
    class="flex items-center justify-center min-h-screen bg-gradient-to-r from-blue-100 to-blue-300"
  >
    <div class="w-full max-w-lg p-8 bg-white rounded-lg shadow-lg">
      <h2 class="mb-6 text-3xl font-semibold text-center text-blue-600">Agendamento</h2>
      <form @submit.prevent="submitForm" class="space-y-6">
        <!-- Campo de Seleção de Serviço -->
        <div>
          <label for="service" class="block mb-2 text-sm font-medium text-gray-700">
            Serviço
          </label>
          <select
            id="service"
            v-model="formData.desiredService"
            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring focus:ring-blue-400 focus:border-blue-400 focus:outline-none"
          >
            <option value="" disabled>Selecione um serviço</option>
            <option v-for="service in services" :key="service.id" :value="service.nameServices">
              {{ service.nameServices }}
            </option>
          </select>
        </div>

        <!-- Campo de Seleção de Profissional -->
        <div>
          <label for="profissional" class="block mb-2 text-sm font-medium text-gray-700">
            Profissional
          </label>
          <select
            id="profissional"
            v-model="formData.barberEnum"
            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring focus:ring-blue-400 focus:border-blue-400 focus:outline-none"
          >
            <option value="0" disabled>Selecione um profissional</option>
            <option v-for="shavy in shavys" :key="shavy.id" :value="shavy.id">
              {{ shavy.barberName }}
            </option>
          </select>
        </div>

        <!-- Campo de Seleção de Data -->
        <div>
          <label for="date" class="block mb-2 text-sm font-medium text-gray-700"> Data </label>
          <input
            type="date"
            id="date"
            v-model="formData.hairCurtDate"
            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring focus:ring-blue-400 focus:border-blue-400 focus:outline-none"
          />
        </div>

        <!-- Campo de Seleção de Hora -->
        <div>
          <label for="time" class="block mb-2 text-sm font-medium text-gray-700"> Hora </label>
          <select
            id="time"
            v-model="formData.time"
            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring focus:ring-blue-400 focus:border-blue-400 focus:outline-none"
          >
            <option value="" disabled>Selecione um horário</option>
            <option v-for="time in availableTimes" :key="time" :value="time">
              {{ time }}
            </option>
          </select>
        </div>

        <!-- Campo Nome do Cliente -->
        <div>
          <label for="clientName" class="block mb-2 text-sm font-medium text-gray-700">
            Nome do Cliente
          </label>
          <input
            type="text"
            id="clientName"
            v-model="formData.clientName"
            placeholder="Digite o nome do cliente"
            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring focus:ring-blue-400 focus:border-blue-400 focus:outline-none"
          />
        </div>

        <!-- Botão de Agendar -->
        <div class="text-center">
          <button
            type="submit"
            class="w-full px-4 py-3 font-semibold text-white transition duration-300 ease-in-out bg-blue-600 rounded-lg hover:bg-blue-700 focus:ring focus:ring-blue-400 focus:outline-none"
          >
            Agendar
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

export default defineComponent({
  name: 'AgendaView',
  setup() {
    const formData = ref({
      idUser: parseInt(localStorage.getItem('idUser') || '0'), // Pega o ID do usuário logado
      hairCurtDate: '',
      desiredService: '',
      time: '',
      barberEnum: 0,
      clientName: localStorage.getItem('userName'),
    })

    // Gerar horários disponíveis
    const generateAvailableTimes = () => {
      const times = []
      const addTimes = (start, end) => {
        let current = start
        while (current <= end) {
          times.push(current)
          const [hours, minutes, seconds] = current.split(':').map(Number)
          const date = new Date(0, 0, 0, hours, minutes + 60, seconds) // Incrementa 1 hora
          current = date.toTimeString().split(' ')[0]
        }
      }
      addTimes('08:00:00', '12:00:00')
      addTimes('14:00:00', '18:00:00')

      return times
    }

    const services = ref([])
    const shavys = ref([])
    const availableTimes = ref(generateAvailableTimes())

    // Função para buscar serviços da API
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
    // Chamada inicial para carregar os serviços

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
          alert('Erro ao buscar os serviços.')
        }
      } catch (error) {
        console.error('Erro:', error)
        alert('Erro ao conectar à API.')
      }
    }

    const getAllEntities = async () => {
      try {
        getAllShavys()
        getAllServices()
      } catch (error) {
        console.error('Erro:', error)
        alert('Erro ao conectar à API.')
      }
    }

    // Chamada inicial para carregar os serviços
    onMounted(getAllEntities)

    const submitForm = async () => {
      const token = localStorage.getItem('authToken')
      if (!token) {
        alert('Usuário não autenticado.')
        return
      }

      console.log(JSON.stringify(formData.value))

      if (
        formData.value.hairCurtDate &&
        formData.value.desiredService &&
        formData.value.time &&
        formData.value.barberEnum
      ) {
        try {
          const response = await fetch('https://localhost:7189/User/Scheduling', {
            method: 'POST',
            headers: {
              Authorization: `Bearer ${token}`,
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData.value),
          })

          if (!response.ok) {
            throw new Error('Erro ao agendar. Tente novamente.')
          }

          alert('Agendamento realizado com sucesso!')
          console.log(await response.json())
        } catch (error) {
          console.error(error)
          alert('Erro ao realizar o agendamento.')
        }
      } else {
        alert('Por favor, preencha todos os campos obrigatórios.')
      }
    }

    return {
      formData,
      services,
      availableTimes,
      shavys,
      submitForm,
    }
  },
})
</script>

<style scoped>
/* Tornar o layout mais amigável em telas menores */
@media (max-width: 640px) {
  .form-container {
    padding: 16px;
  }
}
</style>

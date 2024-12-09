<template>
  <div class="schedule-settings-view">
    <h1>Configurações de Horários</h1>
    <p>Configure os horários de trabalho e agendamentos.</p>
    <div class="actions">
      <button class="btn btn-primary">Novo Horário</button>
      <button class="btn btn-secondary">Ver Configurações</button>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  name: 'ScheduleSettingsView',
}
</script>

<style scoped>
/* Estilo geral da tela de agendamentos */
.schedule-settings-view {
  text-align: center;
  margin: 20px;
  padding: 20px;
  background-color: #f9fafb; /* Fundo cinza claro */
  border-radius: 10px; /* Bordas arredondadas */
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); /* Sombra elegante */
}

/* Título principal */
.schedule-settings-view h1 {
  font-family: 'Roboto', sans-serif; /* Fonte elegante */
  font-size: 2.5rem; /* Tamanho maior */
  color: #2d3748; /* Azul escuro */
  margin-bottom: 10px;
}

/* Descrição abaixo do título */
.schedule-settings-view p {
  font-family: 'Roboto', sans-serif;
  font-size: 1.2rem; /* Tamanho moderado */
  color: #718096; /* Cinza médio */
  margin-bottom: 20px;
}

/* Container de botões */
.actions {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  gap: 15px; /* Espaço entre botões */
  flex-wrap: wrap; /* Ajusta para múltiplas linhas em telas menores */
}

/* Botões principais */
.btn {
  padding: 10px 20px;
  font-size: 1rem;
  font-weight: 500;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.3s ease-in-out;
}

/* Botão primário */
.btn-primary {
  background-color: #4a90e2; /* Azul vibrante */
  color: #ffffff; /* Texto branco */
}

.btn-primary:hover {
  background-color: #357ab7; /* Azul mais escuro no hover */
}

/* Botão secundário */
.btn-secondary {
  background-color: #edf2f7; /* Cinza claro */
  color: #2d3748; /* Azul escuro */
  border: 1px solid #cbd5e0; /* Borda sutil */
}

.btn-secondary:hover {
  background-color: #e2e8f0; /* Cinza mais escuro no hover */
}

/* Responsividade */
@media (max-width: 768px) {
  .schedule-settings-view {
    padding: 15px;
  }

  .btn {
    width: 100%; /* Botões ocupam 100% da largura em telas pequenas */
    text-align: center;
  }
}
</style>

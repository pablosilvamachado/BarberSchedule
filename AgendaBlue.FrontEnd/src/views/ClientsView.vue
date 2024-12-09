<template>
  <div class="clients-view">
    <h1>Clientes</h1>
    <p>Gerencie os dados dos clientes.</p>
    <div class="actions">
      <button class="btn btn-primary">Novo Cliente</button>
      <button class="btn btn-secondary">Ver Todos</button>
    </div>
  </div>
</template>

<style scoped>
/* Estilo geral da tela de clientes */
.clients-view {
  text-align: center;
  margin: 20px;
  padding: 20px;
  background-color: #f9fafb; /* Fundo cinza claro */
  border-radius: 10px; /* Bordas arredondadas */
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); /* Sombra elegante */
}

/* Título principal */
.clients-view h1 {
  font-family: 'Roboto', sans-serif; /* Fonte limpa e moderna */
  font-size: 2.5rem; /* Tamanho do título */
  color: #2d3748; /* Azul escuro */
  margin-bottom: 10px;
}

/* Texto descritivo */
.clients-view p {
  font-family: 'Roboto', sans-serif;
  font-size: 1.2rem;
  color: #718096; /* Cinza médio */
  margin-bottom: 20px;
}

/* Container de botões */
.actions {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  gap: 15px; /* Espaçamento entre botões */
  flex-wrap: wrap; /* Ajuste em várias linhas para telas menores */
}

/* Estilo base dos botões */
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
  background-color: #38a169; /* Verde vibrante */
  color: #ffffff; /* Texto branco */
}

.btn-primary:hover {
  background-color: #2f855a; /* Verde mais escuro no hover */
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
  .clients-view {
    padding: 15px; /* Reduz o padding em telas menores */
  }

  .btn {
    width: 100%; /* Botões ocupam toda a largura */
    text-align: center;
  }
}
</style>

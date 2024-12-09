import axios, { type AxiosResponse, type AxiosInstance  } from 'axios';

// Configura a instância base do Axios
const api: AxiosInstance = axios.create({
    baseURL:  'http://localhost:5000/api', // URL da API
    timeout: 10000, // Tempo limite (10s)
});

// Interceptores para tratar requisições/respostas
api.interceptors.response.use(
    (response: AxiosResponse) => response,
    (error) => {
        console.error('Erro na API:', error.response || error.message);
        return Promise.reject(error);
    }
);

export default api;
